using MediatR;

namespace DatingApp.Application.Features.Admin.Photos.ApprovePhoto;

public record ApprovePhotoCommand(int PhotoId)
    : IRequest<Unit>;