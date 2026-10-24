namespace GymRed.Application.Exceptions;

public class AplicacionException : Exception
{
    public AplicacionException(string message)
        : base(message)
    {
    }

    public AplicacionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
