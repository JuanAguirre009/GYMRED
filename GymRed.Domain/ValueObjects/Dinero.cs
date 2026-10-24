using GymRed.Domain.Exceptions;

namespace GymRed.Domain.ValueObjects;

public sealed class Dinero : IEquatable<Dinero>
{
    public decimal Monto { get; }
    public string Moneda { get; }

    public Dinero(decimal monto, string moneda = "COP")
    {
        if (monto < 0)
            throw new DomainException("El monto no puede ser negativo.");

        if (string.IsNullOrWhiteSpace(moneda))
            throw new DomainException("La moneda es obligatoria.");

        Monto = monto;
        Moneda = moneda.Trim().ToUpperInvariant();
    }

    public bool Equals(Dinero? other)
    {
        if (other is null) return false;
        return Monto == other.Monto && Moneda == other.Moneda;
    }

    public override bool Equals(object? obj) => Equals(obj as Dinero);

    public override int GetHashCode() => HashCode.Combine(Monto, Moneda);

    public static bool operator ==(Dinero? a, Dinero? b)
    {
        if (a is null && b is null) return true;
        if (a is null || b is null) return false;
        return a.Equals(b);
    }

    public static bool operator !=(Dinero? a, Dinero? b) => !(a == b);

    public override string ToString() => $"{Monto:C} {Moneda}";
}
