using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;
using GymRed.Domain.Interfaces;
using GymRed.Domain.ValueObjects;

namespace GymRed.Domain.Entities;

public class Pago : IEntity
{
    public int Id { get; private set; }
    public int MembresiaId { get; private set; }
    public Membresia? Membresia { get; private set; }
    public Dinero Monto { get; private set; } = null!;
    public DateTime FechaPago { get; private set; }
    public MetodoPago MetodoPago { get; private set; }
    public string? NumeroTransaccion { get; private set; }
    public EstadoPago Estado { get; private set; }
    public string? MotivoAnulacion { get; private set; }

    private Pago() { }

    public Pago(int membresiaId, Dinero monto, MetodoPago metodoPago, string? numeroTransaccion = null)
    {
        if (membresiaId <= 0)
            throw new DomainException("La membres�a asociada al pago no es v�lida.");

        MembresiaId = membresiaId;
        Monto = monto ?? throw new DomainException("El monto del pago es obligatorio.");
        FechaPago = DateTime.UtcNow;
        MetodoPago = metodoPago;
        NumeroTransaccion = numeroTransaccion;
        Estado = EstadoPago.Registrado;
    }

    public void Anular(string motivo)
    {
        if (Estado == EstadoPago.Anulado)
            throw new ReglaNegocioException("El pago ya se encuentra anulado.");

        if (string.IsNullOrWhiteSpace(motivo))
            throw new DomainException("El motivo de anulaci�n es obligatorio.");

        Estado = EstadoPago.Anulado;
        MotivoAnulacion = motivo.Trim();
    }
}
