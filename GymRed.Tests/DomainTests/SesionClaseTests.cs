using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;

namespace GymRed.Tests.DomainTests;

public class SesionClaseTests
{
    [Fact]
    public void Sesion_Programada_Esta_En_Estado_Correcto()
    {
        var inicio = DateTime.UtcNow.AddDays(1);
        var fin = inicio.AddHours(1);

        var sesion = new SesionClase(1, 20, inicio, fin);

        Assert.Equal(EstadoSesionClase.Programada, sesion.Estado);
        Assert.True(sesion.EstaProgramada());
    }

    [Fact]
    public void Fecha_Fin_Anterior_A_Inicio_Lanza_Excepcion()
    {
        var inicio = DateTime.UtcNow.AddDays(1);
        var fin = inicio.AddHours(-1);

        Assert.Throws<DomainException>(() => new SesionClase(1, 20, inicio, fin));
    }

    [Fact]
    public void Capacidad_Cero_Lanza_Excepcion()
    {
        var inicio = DateTime.UtcNow.AddDays(1);
        var fin = inicio.AddHours(1);

        Assert.Throws<DomainException>(() => new SesionClase(1, 0, inicio, fin));
    }

    [Fact]
    public void Hay_Cupos_Disponibles_Cuando_Reservas_Son_Menores_A_Capacidad()
    {
        var inicio = DateTime.UtcNow.AddDays(1);
        var sesion = new SesionClase(1, 20, inicio, inicio.AddHours(1));

        Assert.True(sesion.HayCuposDisponibles(19));
        Assert.False(sesion.HayCuposDisponibles(20));
    }

    [Fact]
    public void Cancelar_Sesion_Finalizada_Lanza_Excepcion()
    {
        var inicio = DateTime.UtcNow.AddDays(1);
        var sesion = new SesionClase(1, 20, inicio, inicio.AddHours(1));
        sesion.Finalizar();

        Assert.Throws<ReglaNegocioException>(() => sesion.Cancelar());
    }
}
