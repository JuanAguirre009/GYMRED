using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;
using GymRed.Domain.ValueObjects;

namespace GymRed.Tests.DomainTests;

public class ClienteTests
{
    [Fact]
    public void Crear_Cliente_Valido_Tiene_Estado_Activo()
    {
        Cliente cliente = new Cliente("Juan", "Perez", "123456", "juan@correo.com", "3001234567", new DateTime(1990, 1, 1), 1);

        Assert.Equal("Juan Perez", cliente.NombreCompleto);
        Assert.Equal(EstadoCliente.Activo, cliente.Estado);
        Assert.Empty(cliente.Membresias);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Crear_Cliente_Con_Nombre_Vacio_Lanza_Excepcion(string? nombres)
    {
        Assert.Throws<DomainException>(() =>
            new Cliente(nombres!, "Perez", "123456", "juan@correo.com", "3001234567", new DateTime(1990, 1, 1), 1));
    }

    [Fact]
    public void Asignar_Membresia_Agrega_Al_Historial()
    {
        Cliente cliente = new Cliente("Juan", "Perez", "123456", "juan@correo.com", "3001234567", new DateTime(1990, 1, 1), 1);
        Plan plan = new Plan("Mensual", "Acceso ilimitado 30 dias", 30, new Dinero(100_000), "Acceso a todas las sedes");

        Membresia membresia = cliente.AsignarMembresia(plan);

        Assert.Single(cliente.Membresias);
        Assert.Equal(plan.Id, membresia.PlanId);
        Assert.True(cliente.TieneMembresiaVigente(DateTime.UtcNow.Date));
    }

    [Fact]
    public void Cambiar_Estado_Cliente_Actualiza_Correctamente()
    {
        Cliente cliente = new Cliente("Juan", "Perez", "123456", "juan@correo.com", "3001234567", new DateTime(1990, 1, 1), 1);

        cliente.CambiarEstado(EstadoCliente.Suspendido);

        Assert.Equal(EstadoCliente.Suspendido, cliente.Estado);
    }

    [Fact]
    public void Asignar_Membresia_A_Cliente_Inactivo_Lanza_Excepcion()
    {
        Cliente cliente = new Cliente("Juan", "Perez", "123456", "juan@correo.com", "3001234567", new DateTime(1990, 1, 1), 1);
        cliente.CambiarEstado(EstadoCliente.Inactivo);
        Plan plan = new Plan("Mensual", "Acceso ilimitado 30 dias", 30, new Dinero(100_000), "Acceso a todas las sedes");

        Assert.Throws<ReglaNegocioException>(() => cliente.AsignarMembresia(plan));
    }
}
