using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;
using GymRed.Domain.ValueObjects;

namespace GymRed.Tests.DomainTests;

public class MembresiaTests
{
    private static Cliente CrearClienteTest() =>
        new("Juan", "P�rez", "123456", "juan@correo.com", "3001234567", new DateTime(1990, 1, 1), 1);

    [Fact]
    public void Membresia_Calcula_Fecha_Fin_Segun_Duracion_Del_Plan()
    {
        var cliente = CrearClienteTest();
        var plan = new Plan("Mensual", "Plan 30 d�as", 30, new Dinero(100_000), "Acceso general");
        var fechaInicio = new DateTime(2026, 1, 1);

        var membresia = new Membresia(cliente, plan, fechaInicio);

        Assert.Equal(fechaInicio, membresia.FechaInicio);
        Assert.Equal(new DateTime(2026, 1, 30), membresia.FechaFin);
        Assert.Equal(EstadoMembresia.Activa, membresia.Estado);
    }

    [Fact]
    public void Membresia_Esta_Vigente_Dentro_Del_Periodo()
    {
        var cliente = CrearClienteTest();
        var plan = new Plan("Mensual", "Plan 30 d�as", 30, new Dinero(100_000), "Acceso general");
        var fechaInicio = DateTime.UtcNow.Date.AddDays(-5);

        var membresia = new Membresia(cliente, plan, fechaInicio);

        Assert.True(membresia.EstaVigente(DateTime.UtcNow.Date));
    }

    [Fact]
    public void Membresia_No_Esta_Vigente_Fuera_Del_Periodo()
    {
        var cliente = CrearClienteTest();
        var plan = new Plan("Mensual", "Plan 30 d�as", 30, new Dinero(100_000), "Acceso general");
        var fechaInicio = DateTime.UtcNow.Date.AddDays(-60);

        var membresia = new Membresia(cliente, plan, fechaInicio);

        Assert.False(membresia.EstaVigente(DateTime.UtcNow.Date));
    }

    [Fact]
    public void Asignar_Plan_Inactivo_Lanza_Excepcion()
    {
        var cliente = CrearClienteTest();
        var plan = new Plan("Mensual", "Plan 30 d�as", 30, new Dinero(100_000), "Acceso general");
        plan.CambiarEstado(false);

        Assert.Throws<ReglaNegocioException>(() => new Membresia(cliente, plan, DateTime.UtcNow.Date));
    }

    [Fact]
    public void Registrar_Pago_En_Membresia_Cancelada_Lanza_Excepcion()
    {
        var cliente = CrearClienteTest();
        var plan = new Plan("Mensual", "Plan 30 d�as", 30, new Dinero(100_000), "Acceso general");
        var membresia = new Membresia(cliente, plan, DateTime.UtcNow.Date);
        membresia.Cancelar();

        Assert.Throws<ReglaNegocioException>(() =>
            membresia.RegistrarPago(new Dinero(100_000), MetodoPago.Efectivo));
    }
}
