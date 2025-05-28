namespace DatingApp.Application.DTOs.Messages;

public class MessageParams : PaginationParams
{
    public string? Username { get; set; }
    public string Container { get; set; } = "Unread";
}
