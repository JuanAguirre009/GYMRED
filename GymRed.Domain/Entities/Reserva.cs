using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;
using GymRed.Domain.Interfaces;

namespace GymRed.Domain.Entities;

public class Reserva : IEntity
{
    public int Id { get; private set; }
    public int SesionClaseId { get; private set; }
    public SesionClase? SesionClase { get; private set; }
    public int ClienteId { get; private set; }
    public Cliente? Cliente { get; private set; }
    public DateTime FechaReserva { get; private set; }
    public EstadoReserva Estado { get; private set; }

    private Reserva() { }

    public Reserva(int sesionClaseId, int clienteId)
    {
        if (sesionClaseId <= 0)
            throw new DomainException("La sesion de clase no es valida.");

        if (clienteId <= 0)
            throw new DomainException("El cliente de la reserva no es valido.");

        SesionClaseId = sesionClaseId;
        ClienteId = clienteId;
        FechaReserva = DateTime.UtcNow;
        Estado = EstadoReserva.Activa;
    }

    public void Cancelar()
    {
        if (Estado == EstadoReserva.Cancelada)
            throw new ReglaNegocioException("La reserva ya esta cancelada.");

        if (Estado == EstadoReserva.Cumplida)
            throw new ReglaNegocioException("No se puede cancelar una reserva ya cumplida.");

        Estado = EstadoReserva.Cancelada;
    }

    public void MarcarComoCumplida()
    {
        if (Estado == EstadoReserva.Cancelada)
            throw new ReglaNegocioException("No se puede marcar como cumplida una reserva cancelada.");

        Estado = EstadoReserva.Cumplida;
    }
}
