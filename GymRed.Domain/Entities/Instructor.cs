using GymRed.Domain.Exceptions;
using GymRed.Domain.Interfaces;

namespace GymRed.Domain.Entities;

public class Instructor : IEntity
{
    public int Id { get; private set; }
    public string Nombres { get; private set; } = string.Empty;
    public string Apellidos { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Telefono { get; private set; } = string.Empty;
    public string Especialidad { get; private set; } = string.Empty;
    public bool Activo { get; private set; }

    private Instructor() { }

    public Instructor(string nombres, string apellidos, string email, string telefono, string especialidad)
    {
        ValidarNombres(nombres);
        ValidarApellidos(apellidos);
        ValidarEmail(email);
        ValidarTelefono(telefono);
        ValidarEspecialidad(especialidad);

        Nombres = nombres.Trim();
        Apellidos = apellidos.Trim();
        Email = email.Trim().ToLowerInvariant();
        Telefono = telefono.Trim();
        Especialidad = especialidad.Trim();
        Activo = true;
    }

    public void ActualizarDatos(string nombres, string apellidos, string email, string telefono, string especialidad)
    {
        ValidarNombres(nombres);
        ValidarApellidos(apellidos);
        ValidarEmail(email);
        ValidarTelefono(telefono);
        ValidarEspecialidad(especialidad);

        Nombres = nombres.Trim();
        Apellidos = apellidos.Trim();
        Email = email.Trim().ToLowerInvariant();
        Telefono = telefono.Trim();
        Especialidad = especialidad.Trim();
    }

    public void CambiarEstado(bool activo)
    {
        Activo = activo;
    }

    public string NombreCompleto => $"{Nombres} {Apellidos}";

    private static void ValidarNombres(string nombres)
    {
        if (string.IsNullOrWhiteSpace(nombres))
            throw new DomainException("Los nombres del instructor son obligatorios.");
    }

    private static void ValidarApellidos(string apellidos)
    {
        if (string.IsNullOrWhiteSpace(apellidos))
            throw new DomainException("Los apellidos del instructor son obligatorios.");
    }

    private static void ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("El email del instructor es obligatorio.");
    }

    private static void ValidarTelefono(string telefono)
    {
        if (string.IsNullOrWhiteSpace(telefono))
            throw new DomainException("El telefono del instructor es obligatorio.");
    }

    private static void ValidarEspecialidad(string especialidad)
    {
        if (string.IsNullOrWhiteSpace(especialidad))
            throw new DomainException("La especialidad del instructor es obligatoria.");
    }
}
