using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;
using GymRed.Domain.Interfaces;

namespace GymRed.Domain.Entities;

public class SesionClase : IEntity
{
    public int Id { get; private set; }
    public int ClaseId { get; private set; }
    public Clase? Clase { get; private set; }
    public DateTime FechaHoraInicio { get; private set; }
    public DateTime FechaHoraFin { get; private set; }
    public int CapacidadMaxima { get; private set; }
    public EstadoSesionClase Estado { get; private set; }

    private SesionClase() { }

    public SesionClase(int claseId, int capacidadMaxima, DateTime fechaHoraInicio, DateTime fechaHoraFin)
    {
        if (claseId <= 0)
            throw new DomainException("La clase asociada a la sesi�n no es v�lida.");

        ValidarCapacidad(capacidadMaxima);
        ValidarFechas(fechaHoraInicio, fechaHoraFin);

        ClaseId = claseId;
        CapacidadMaxima = capacidadMaxima;
        FechaHoraInicio = fechaHoraInicio;
        FechaHoraFin = fechaHoraFin;
        Estado = EstadoSesionClase.Programada;
    }

    public bool EstaProgramada()
    {
        return Estado == EstadoSesionClase.Programada
            && FechaHoraInicio > DateTime.UtcNow;
    }

    public bool HayCuposDisponibles(int reservasActivas)
    {
        return reservasActivas < CapacidadMaxima;
    }

    public void Cancelar()
    {
        if (Estado == EstadoSesionClase.Finalizada)
            throw new ReglaNegocioException("No se puede cancelar una sesi�n ya finalizada.");

        Estado = EstadoSesionClase.Cancelada;
    }

    public void Finalizar()
    {
        if (Estado == EstadoSesionClase.Cancelada)
            throw new ReglaNegocioException("No se puede finalizar una sesi�n cancelada.");

        Estado = EstadoSesionClase.Finalizada;
    }

    private static void ValidarCapacidad(int capacidadMaxima)
    {
        if (capacidadMaxima <= 0)
            throw new DomainException("La capacidad de la sesi�n debe ser mayor a cero.");
    }

    private static void ValidarFechas(DateTime inicio, DateTime fin)
    {
        if (fin <= inicio)
            throw new DomainException("La fecha/hora de fin debe ser posterior a la de inicio.");
    }
}
