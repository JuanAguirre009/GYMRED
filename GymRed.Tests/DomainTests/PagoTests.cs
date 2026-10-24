using GymRed.Domain.Entities;
using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;
using GymRed.Domain.ValueObjects;

namespace GymRed.Tests.DomainTests;

public class PagoTests
{
    [Fact]
    public void Pago_Nuevo_Tiene_Estado_Registrado()
    {
        Pago pago = new Pago(1, new Dinero(100_000), MetodoPago.Efectivo, "TX-001");

        Assert.Equal(EstadoPago.Registrado, pago.Estado);
        Assert.Equal("TX-001", pago.NumeroTransaccion);
    }

    [Fact]
    public void Anular_Pago_Requiere_Motivo()
    {
        Pago pago = new Pago(1, new Dinero(100_000), MetodoPago.Efectivo);

        Assert.Throws<DomainException>(() => pago.Anular("   "));
    }

    [Fact]
    public void Anular_Pago_Cambia_Estado_Y_Guarda_Motivo()
    {
        Pago pago = new Pago(1, new Dinero(100_000), MetodoPago.Efectivo);

        pago.Anular("Pago duplicado");

        Assert.Equal(EstadoPago.Anulado, pago.Estado);
        Assert.Equal("Pago duplicado", pago.MotivoAnulacion);
    }

    [Fact]
    public void Anular_Pago_Dos_Veces_Lanza_Excepcion()
    {
        Pago pago = new Pago(1, new Dinero(100_000), MetodoPago.Efectivo);
        pago.Anular("Pago duplicado");

        Assert.Throws<ReglaNegocioException>(() => pago.Anular("Motivo repetido"));
    }
}
