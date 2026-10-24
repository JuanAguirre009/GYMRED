namespace GymRed.Application.DTOs;

public class RegistrarPagoDto
{
    public int MembresiaId { get; set; }
    public decimal Monto { get; set; }
    public string Moneda { get; set; } = "COP";
    public string MetodoPago { get; set; } = string.Empty;
    public string? NumeroTransaccion { get; set; }
}
