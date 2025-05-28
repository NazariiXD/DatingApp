using MediatR;

namespace DatingApp.Application.Features.Users.Photos.DeletePhoto;

public record DeletePhotoCommand(int PhotoId, string Username)
    : IRequest<Unit>;