namespace GymRed.Application.DTOs;

public record class PagoDto(
    int Id,
    int MembresiaId,
    decimal Monto,
    string Moneda,
    DateTime FechaPago,
    string MetodoPago,
    string? NumeroTransaccion,
    string Estado,
    string? MotivoAnulacion);
