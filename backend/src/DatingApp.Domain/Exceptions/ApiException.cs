namespace DatingApp.Domain.Exceptions;

public class ApiException(int statusCode, string message, string? details) : Exception(message)
{
    public int StatusCode { get; set; } = statusCode;
    public string? Details { get; set; } = details;
}
