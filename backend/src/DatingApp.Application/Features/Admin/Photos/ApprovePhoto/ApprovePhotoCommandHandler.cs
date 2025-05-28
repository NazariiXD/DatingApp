using DatingApp.Application.Repositories;
using DatingApp.Domain.Exceptions;
using MediatR;

namespace DatingApp.Application.Features.Admin.Photos.ApprovePhoto;

public class ApprovePhotoCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<ApprovePhotoCommand, Unit>
{
    public async Task<Unit> Handle(ApprovePhotoCommand request, CancellationToken cancellationToken)
    {
        var photoId = request.PhotoId;

        var photo = await unitOfWork.PhotoRepository.GetPhotoById(photoId);

        if (photo == null)
        {
            throw new NotFoundException("Photo not found");
        }

        photo.IsApproved = true;

        var user = await unitOfWork.UserRepository.GetUserByPhotoId(photoId);

        if (user == null)
        {
            throw new NotFoundException("Could not get user from db");
        }

        if (!user.Photos.Any(x => x.IsMain)) photo.IsMain = true;

        await unitOfWork.Complete();

        return Unit.Value;
    }
}