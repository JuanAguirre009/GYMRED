using GymRed.Domain.Exceptions;
using GymRed.Domain.Interfaces;

namespace GymRed.Domain.Entities;

public class Asistencia : IEntity
{
    public int Id { get; private set; }
    public int SesionClaseId { get; private set; }
    public SesionClase? SesionClase { get; private set; }
    public int ClienteId { get; private set; }
    public Cliente? Cliente { get; private set; }
    public DateTime FechaHoraRegistro { get; private set; }
    public bool Asistio { get; private set; }

    private Asistencia() { }

    public Asistencia(int sesionClaseId, int clienteId, bool asistio = true)
    {
        if (sesionClaseId <= 0)
            throw new DomainException("La sesion de clase no es valida.");

        if (clienteId <= 0)
            throw new DomainException("El cliente de la asistencia no es valido.");

        SesionClaseId = sesionClaseId;
        ClienteId = clienteId;
        FechaHoraRegistro = DateTime.UtcNow;
        Asistio = asistio;
    }
}
