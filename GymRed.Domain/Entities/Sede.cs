using GymRed.Domain.Exceptions;
using GymRed.Domain.Interfaces;

namespace GymRed.Domain.Entities;

public class Sede : IEntity, IAggregateRoot
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Direccion { get; private set; } = string.Empty;
    public string Telefono { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public DateTime FechaApertura { get; private set; }
    public bool Activa { get; private set; }

    private Sede() { }

    public Sede(string nombre, string direccion, string telefono, string email, DateTime fechaApertura)
    {
        ValidarNombre(nombre);
        ValidarDireccion(direccion);
        ValidarTelefono(telefono);
        ValidarEmail(email);

        Nombre = nombre.Trim();
        Direccion = direccion.Trim();
        Telefono = telefono.Trim();
        Email = email.Trim().ToLowerInvariant();
        FechaApertura = fechaApertura.Date;
        Activa = true;
    }

    public void Actualizar(string nombre, string direccion, string telefono, string email)
    {
        ValidarNombre(nombre);
        ValidarDireccion(direccion);
        ValidarTelefono(telefono);
        ValidarEmail(email);

        Nombre = nombre.Trim();
        Direccion = direccion.Trim();
        Telefono = telefono.Trim();
        Email = email.Trim().ToLowerInvariant();
    }

    public void CambiarEstado(bool activa)
    {
        Activa = activa;
    }

    private static void ValidarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre de la sede es obligatorio.");
    }

    private static void ValidarDireccion(string direccion)
    {
        if (string.IsNullOrWhiteSpace(direccion))
            throw new DomainException("La direcci�n de la sede es obligatoria.");
    }

    private static void ValidarTelefono(string telefono)
    {
        if (string.IsNullOrWhiteSpace(telefono))
            throw new DomainException("El tel�fono de la sede es obligatorio.");
    }

    private static void ValidarEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("El email de la sede es obligatorio.");
    }
}
