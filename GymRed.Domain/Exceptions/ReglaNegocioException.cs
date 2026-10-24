namespace GymRed.Domain.Exceptions;

public class ReglaNegocioException : DomainException
{
    public ReglaNegocioException(string message)
        : base(message)
    {
    }
}
