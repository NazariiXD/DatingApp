namespace DatingApp.Application.DTOs.Users.Photos;

public class PhotoForApprovalDto
{
    public int Id { get; set; }
    public required string Url { get; set; }
    public string? Username { get; set; }
    public bool IsApproved { get; set; }
}
