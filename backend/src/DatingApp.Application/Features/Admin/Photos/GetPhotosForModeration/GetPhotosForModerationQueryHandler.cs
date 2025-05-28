using DatingApp.Application.DTOs.Users.Photos;
using DatingApp.Application.Repositories;
using MediatR;

namespace DatingApp.Application.Features.Admin.Photos.GetPhotosForModeration;

public class GetPhotosForModerationQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetPhotosForModerationQuery, IEnumerable<PhotoForApprovalDto>>
{
    public async Task<IEnumerable<PhotoForApprovalDto>> Handle(GetPhotosForModerationQuery request, CancellationToken cancellationToken)
    {
        var photos = await unitOfWork.PhotoRepository.GetUnapprovedPhotos();

        return photos;
    }
}