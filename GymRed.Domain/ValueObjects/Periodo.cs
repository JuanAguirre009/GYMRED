using GymRed.Domain.Exceptions;

namespace GymRed.Domain.ValueObjects;

public sealed class Periodo : IEquatable<Periodo>
{
    public DateTime FechaInicio { get; }
    public DateTime FechaFin { get; }

    public Periodo(DateTime fechaInicio, DateTime fechaFin)
    {
        if (fechaFin < fechaInicio)
            throw new DomainException("La fecha de finalizaci�n debe ser mayor o igual a la fecha de inicio.");

        FechaInicio = fechaInicio.Date;
        FechaFin = fechaFin.Date;
    }

    public bool Incluye(DateTime fecha)
    {
        DateTime fechaNormalizada = fecha.Date;
        return fechaNormalizada >= FechaInicio && fechaNormalizada <= FechaFin;
    }

    public int DuracionEnDias => (FechaFin - FechaInicio).Days + 1;

    public bool Equals(Periodo? other)
    {
        if (other is null) return false;
        return FechaInicio == other.FechaInicio && FechaFin == other.FechaFin;
    }

    public override bool Equals(object? obj) => Equals(obj as Periodo);

    public override int GetHashCode() => HashCode.Combine(FechaInicio, FechaFin);

    public override string ToString() => $"{FechaInicio:yyyy-MM-dd} - {FechaFin:yyyy-MM-dd}";
}
