namespace DatingApp.Domain.Exceptions;

public class NotFoundException : ApiException
{
    public NotFoundException(string message)
        : base(400, message, null)
    {
    }
}