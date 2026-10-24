using GymRed.Application.DTOs;
using GymRed.Domain.Entities;

namespace GymRed.Application.Mappings;

public static class PagoMapping
{
    public static PagoDto ToDto(this Pago pago)
    {
        return new PagoDto(
            pago.Id,
            pago.MembresiaId,
            pago.Monto.Monto,
            pago.Monto.Moneda,
            pago.FechaPago,
            pago.MetodoPago.ToString(),
            pago.NumeroTransaccion,
            pago.Estado.ToString(),
            pago.MotivoAnulacion);
    }
}
