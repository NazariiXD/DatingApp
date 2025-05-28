using MediatR;

namespace DatingApp.Application.Features.Admin.Photos.RejectPhoto;

public record RejectPhotoCommand(int PhotoId)
    : IRequest<Unit>;