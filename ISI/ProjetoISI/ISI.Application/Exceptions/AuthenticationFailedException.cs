namespace ISI.Application.Exceptions;

public class AuthenticationFailedException: ApplicationException
{
    public AuthenticationFailedException(string? message) : base(message)
    {
    }
}