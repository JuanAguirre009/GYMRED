using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;

namespace GymRed.Tests.DomainTests;

public class ReservaTests
{
    [Fact]
    public void Reserva_Nueva_Tiene_Estado_Activo()
    {
        var reserva = new Reserva(1, 1);

        Assert.Equal(EstadoReserva.Activa, reserva.Estado);
    }

    [Fact]
    public void Cancelar_Reserva_Activa_Cambia_Estado()
    {
        var reserva = new Reserva(1, 1);

        reserva.Cancelar();

        Assert.Equal(EstadoReserva.Cancelada, reserva.Estado);
    }

    [Fact]
    public void Cancelar_Reserva_Ya_Cancelada_Lanza_Excepcion()
    {
        var reserva = new Reserva(1, 1);
        reserva.Cancelar();

        Assert.Throws<ReglaNegocioException>(() => reserva.Cancelar());
    }

    [Fact]
    public void Marcar_Como_Cumplida_Reserva_Cancelada_Lanza_Excepcion()
    {
        var reserva = new Reserva(1, 1);
        reserva.Cancelar();

        Assert.Throws<ReglaNegocioException>(() => reserva.MarcarComoCumplida());
    }
}
