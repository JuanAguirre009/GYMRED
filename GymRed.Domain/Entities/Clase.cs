using GymRed.Domain.Exceptions;
using GymRed.Domain.Interfaces;

namespace GymRed.Domain.Entities;

public class Clase : IEntity
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Descripcion { get; private set; } = string.Empty;
    public int InstructorId { get; private set; }
    public Instructor? Instructor { get; private set; }
    public int SedeId { get; private set; }
    public Sede? Sede { get; private set; }
    public int CapacidadMaxima { get; private set; }
    public bool Activa { get; private set; }

    private readonly List<SesionClase> _sesiones = new();
    public IReadOnlyCollection<SesionClase> Sesiones => _sesiones.AsReadOnly();

    private Clase() { }

    public Clase(string nombre, string descripcion, int instructorId, int sedeId, int capacidadMaxima)
    {
        ValidarNombre(nombre);
        ValidarDescripcion(descripcion);
        ValidarInstructorId(instructorId);
        ValidarSedeId(sedeId);
        ValidarCapacidad(capacidadMaxima);

        Nombre = nombre.Trim();
        Descripcion = descripcion.Trim();
        InstructorId = instructorId;
        SedeId = sedeId;
        CapacidadMaxima = capacidadMaxima;
        Activa = true;
    }

    public void Actualizar(string nombre, string descripcion, int instructorId, int sedeId, int capacidadMaxima)
    {
        ValidarNombre(nombre);
        ValidarDescripcion(descripcion);
        ValidarInstructorId(instructorId);
        ValidarSedeId(sedeId);
        ValidarCapacidad(capacidadMaxima);

        Nombre = nombre.Trim();
        Descripcion = descripcion.Trim();
        InstructorId = instructorId;
        SedeId = sedeId;
        CapacidadMaxima = capacidadMaxima;
    }

    public void CambiarEstado(bool activa)
    {
        Activa = activa;
    }

    public SesionClase ProgramarSesion(DateTime fechaHoraInicio, DateTime fechaHoraFin)
    {
        var sesion = new SesionClase(Id, CapacidadMaxima, fechaHoraInicio, fechaHoraFin);
        _sesiones.Add(sesion);
        return sesion;
    }

    private static void ValidarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre de la clase es obligatorio.");
    }

    private static void ValidarDescripcion(string descripcion)
    {
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new DomainException("La descripci�n de la clase es obligatoria.");
    }

    private static void ValidarInstructorId(int instructorId)
    {
        if (instructorId <= 0)
            throw new DomainException("El instructor de la clase no es v�lido.");
    }

    private static void ValidarSedeId(int sedeId)
    {
        if (sedeId <= 0)
            throw new DomainException("La sede de la clase no es v�lida.");
    }

    private static void ValidarCapacidad(int capacidadMaxima)
    {
        if (capacidadMaxima <= 0)
            throw new DomainException("La capacidad m�xima debe ser mayor a cero.");
    }
}
