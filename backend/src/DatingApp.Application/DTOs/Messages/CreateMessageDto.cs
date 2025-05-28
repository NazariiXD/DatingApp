namespace DatingApp.Application.DTOs.Messages;

public class CreateMessageDto
{
    public required string RecipientUsername { get; set; }
    public required string Content { get; set; }
}
