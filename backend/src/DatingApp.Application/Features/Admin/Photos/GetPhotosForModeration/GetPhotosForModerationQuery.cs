using DatingApp.Application.DTOs.Users.Photos;
using MediatR;

namespace DatingApp.Application.Features.Admin.Photos.GetPhotosForModeration;

public record GetPhotosForModerationQuery()
    : IRequest<IEnumerable<PhotoForApprovalDto>>;