using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;
using GymRed.Domain.Interfaces;
using GymRed.Domain.ValueObjects;

namespace GymRed.Domain.Entities;

public class Membresia : IEntity
{
    public int Id { get; private set; }
    public int ClienteId { get; private set; }
    public Cliente? Cliente { get; private set; }
    public int PlanId { get; private set; }
    public Plan? Plan { get; private set; }
    public DateTime FechaInicio { get; private set; }
    public DateTime FechaFin { get; private set; }
    public EstadoMembresia Estado { get; private set; }
    public Dinero PrecioPagado { get; private set; } = null!;

    private readonly List<Pago> _pagos = new();
    public IReadOnlyCollection<Pago> Pagos => _pagos.AsReadOnly();

    private Membresia() { }

    public Membresia(Cliente cliente, Plan plan, DateTime fechaInicio)
    {
        if (cliente is null)
            throw new DomainException("El cliente de la membresia es obligatorio.");

        if (plan is null)
            throw new DomainException("El plan de la membresia es obligatorio.");

        if (!plan.Activo)
            throw new ReglaNegocioException("No se puede asignar un plan inactivo.");

        ClienteId = cliente.Id;
        Cliente = cliente;
        PlanId = plan.Id;
        Plan = plan;
        FechaInicio = fechaInicio.Date;
        FechaFin = CalcularFechaFin(fechaInicio, plan.DuracionDias);
        Estado = EstadoMembresia.Activa;
        PrecioPagado = plan.Precio;
    }

    public bool EstaVigente(DateTime fecha)
    {
        DateTime fechaConsulta = fecha.Date;
        return Estado == EstadoMembresia.Activa
            && fechaConsulta >= FechaInicio
            && fechaConsulta <= FechaFin;
    }

    public void MarcarComoVencida()
    {
        if (Estado != EstadoMembresia.Activa)
            return;

        if (DateTime.UtcNow.Date > FechaFin)
            Estado = EstadoMembresia.Vencida;
    }

    public void Cancelar()
    {
        if (Estado == EstadoMembresia.Vencida)
            throw new ReglaNegocioException("No se puede cancelar una membresia ya vencida.");

        Estado = EstadoMembresia.Cancelada;
    }

    public Pago RegistrarPago(Dinero monto, MetodoPago metodoPago, string? numeroTransaccion = null)
    {
        if (Estado == EstadoMembresia.Cancelada)
            throw new ReglaNegocioException("No se pueden registrar pagos en una membresia cancelada.");

        Pago pago = new Pago(Id, monto, metodoPago, numeroTransaccion);
        _pagos.Add(pago);

        return pago;
    }

    private static DateTime CalcularFechaFin(DateTime fechaInicio, int duracionDias)
    {
        return fechaInicio.Date.AddDays(duracionDias - 1);
    }
}
