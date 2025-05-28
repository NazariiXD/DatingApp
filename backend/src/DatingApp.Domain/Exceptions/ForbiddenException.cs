namespace DatingApp.Domain.Exceptions;

public class ForbiddenException : ApiException
{
    public ForbiddenException(string message)
        : base(403, message, "Forbidden access")
    {
    }
}