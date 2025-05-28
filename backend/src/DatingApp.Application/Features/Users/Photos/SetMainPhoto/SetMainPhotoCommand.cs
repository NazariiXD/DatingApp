using MediatR;

namespace DatingApp.Application.Features.Users.Photos.SetMainPhoto;

public record SetMainPhotoCommand(int PhotoId, string Username)
    : IRequest<Unit>;