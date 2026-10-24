using GymRed.Domain.Exceptions;
using GymRed.Domain.Interfaces;
using GymRed.Domain.ValueObjects;

namespace GymRed.Domain.Entities;

public class Plan : IEntity
{
    public int Id { get; private set; }
    public string Nombre { get; private set; } = string.Empty;
    public string Descripcion { get; private set; } = string.Empty;
    public int DuracionDias { get; private set; }
    public Dinero Precio { get; private set; } = null!;
    public string Beneficios { get; private set; } = string.Empty;
    public bool Activo { get; private set; }

    private Plan() { }

    public Plan(string nombre, string descripcion, int duracionDias, Dinero precio, string beneficios)
    {
        ValidarNombre(nombre);
        ValidarDescripcion(descripcion);
        ValidarDuracion(duracionDias);
        ValidarBeneficios(beneficios);

        Nombre = nombre.Trim();
        Descripcion = descripcion.Trim();
        DuracionDias = duracionDias;
        Precio = precio ?? throw new DomainException("El precio del plan es obligatorio.");
        Beneficios = beneficios.Trim();
        Activo = true;
    }

    public void Actualizar(string nombre, string descripcion, int duracionDias, Dinero precio, string beneficios)
    {
        ValidarNombre(nombre);
        ValidarDescripcion(descripcion);
        ValidarDuracion(duracionDias);
        ValidarBeneficios(beneficios);

        Nombre = nombre.Trim();
        Descripcion = descripcion.Trim();
        DuracionDias = duracionDias;
        Precio = precio ?? throw new DomainException("El precio del plan es obligatorio.");
        Beneficios = beneficios.Trim();
    }

    public void CambiarEstado(bool activo)
    {
        Activo = activo;
    }

    private static void ValidarNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new DomainException("El nombre del plan es obligatorio.");
    }

    private static void ValidarDescripcion(string descripcion)
    {
        if (string.IsNullOrWhiteSpace(descripcion))
            throw new DomainException("La descripci�n del plan es obligatoria.");
    }

    private static void ValidarDuracion(int duracionDias)
    {
        if (duracionDias <= 0)
            throw new DomainException("La duraci�n del plan debe ser mayor a cero d�as.");
    }

    private static void ValidarBeneficios(string beneficios)
    {
        if (string.IsNullOrWhiteSpace(beneficios))
            throw new DomainException("Los beneficios del plan son obligatorios.");
    }
}
