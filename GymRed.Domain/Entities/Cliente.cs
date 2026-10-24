using GymRed.Domain.Enums;
using GymRed.Domain.Exceptions;
using GymRed.Domain.Interfaces;

namespace GymRed.Domain.Entities;

public class Cliente : IEntity, IAggregateRoot
{
    public int Id { get; private set; }
    public string Nombres { get; private set; } = string.Empty;
    public string Apellidos { get; private set; } = string.Empty;
    public string NumeroIdentificacion { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Telefono { get; private set; } = string.Empty;
    public DateTime FechaNacimiento { get; private set; }
    public DateTime FechaRegistro { get; private set; }
    public EstadoCliente Estado { get; private set; }
    public int SedeId { get; private set; }
    public Sede? Sede { get; private set; }

    private readonly List<Membresia> _membresias = new();
    public IReadOnlyCollection<Membresia> Membresias => _membresias.AsReadOnly();

    private readonly List<Reserva> _reservas = new();
    public IReadOnlyCollection<Reserva> Reservas => _reservas.AsReadOnly();

    private readonly List<Asistencia> _asistencias = new();
    public IReadOnlyCollection<Asistencia> Asistencias => _asistencias.AsReadOnly();

    private Cliente() { }

    public Cliente(string nombres, string apellidos, string numeroIdentificacion, string email, string telefono, DateTime fechaNacimiento, int sedeId)
    {
        ValidarNombres(nombres);
        ValidarApellidos(apellidos);
        ValidarIdentificacion(numeroIdentificacion);
        ValidarEmail(email);
        ValidarTelefono(telefono);
        ValidarFechaNacimiento(fechaNacimiento);
        ValidarSedeId(sedeId);

        Nombres = nombres.Trim();
        Apellidos = apellidos.Trim();
        NumeroIdentificacion = numeroIdentificacion.Trim();
        Email = email.Trim().ToLowerInvariant();
        Telefono = telefono.Trim();
        FechaNacimiento = fechaNacimiento.Date;
        FechaRegistro = DateTime.UtcNow;
        Estado = EstadoCliente.Activo;
        SedeId = sedeId;
    }

    public void ActualizarDatos(string nombres, string apellidos, string email, string telefono)
    {
        ValidarNombres(nombres);
        ValidarApellidos(apellidos);
        ValidarEmail(email);
        ValidarTelefono(telefono);

        Nombres = nombres.Trim();
        Apellidos = apellidos.Trim();
        Email = email.Trim().ToLowerInvariant();
        Telefono = telefono.Trim();
    }

    public void CambiarEstado(EstadoCliente nuevoEstado)
    {
        Estado = nuevoEstado;
    }

    public Membresia AsignarMembresia(Plan plan)
    {
        if (plan is null)
            throw new DomainException("El plan es obligatorio para asignar una membres�a.");

        var fechaInicio = DateTime.UtcNow.Date;
        var membresia = new Membresia(this, plan, fechaInicio);
        _membresias.Add(membresia);

        return membresia;
    }

    public bool TieneMembresiaVigente(DateTime fecha)
    {
        return _membresias.Any(m => m.EstaVigente(fecha));
    }

    public string NombreCompleto => $"{Nombres} {Apellidos}";

    private static void ValidarNombres(string nombres)
    {
        if (string.IsNullOrWhiteSpace(nombres))
            throw new DomainException("Los nombres del cliente son obligatorios.");
    }

    private static void ValidarApellidos(string apellidos)
    {
        if (string.IsNullOrWhiteSpace(apellidos))
            throw new DomainException("Los apellidos del cliente son obligatorios.");
    }

    private static void ValidarIdentificacion(string numeroIdentificacion)
    {
        if (string.IsNullOrWhiteSpace(numeroIdentificacion))
            throw new DomainException("El n�mero de identificaci�n del cliente es obligatorio.");
    }

    private static void ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("El email del cliente es obligatorio.");
    }

    private static void ValidarTelefono(string telefono)
    {
        if (string.IsNullOrWhiteSpace(telefono))
            throw new DomainException("El tel�fono del cliente es obligatorio.");
    }

    private static void ValidarFechaNacimiento(DateTime fechaNacimiento)
    {
        if (fechaNacimiento.Date > DateTime.UtcNow.Date)
            throw new DomainException("La fecha de nacimiento no puede ser futura.");
    }

    private static void ValidarSedeId(int sedeId)
    {
        if (sedeId <= 0)
            throw new DomainException("La sede asociada al cliente no es v�lida.");
    }
}
