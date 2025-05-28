using DatingApp.Application.Interfaces;
using DatingApp.Application.Repositories;
using DatingApp.Domain.Exceptions;
using MediatR;

namespace DatingApp.Application.Features.Admin.Photos.RejectPhoto;

public class RejectPhotoCommandHandler(
    IUnitOfWork unitOfWork,
    IPhotoService photoService)
    : IRequestHandler<RejectPhotoCommand, Unit>
{
    public async Task<Unit> Handle(RejectPhotoCommand request, CancellationToken cancellationToken)
    {
        var photo = await unitOfWork.PhotoRepository.GetPhotoById(request.PhotoId);

        if (photo == null)
        {
            throw new NotFoundException("Photo not found");
        }

        if (photo.PublicId != null)
        {
            var result = await photoService.DeletePhotoAsync(photo.PublicId);

            if (result.Result == "ok")
            {
                unitOfWork.PhotoRepository.RemovePhoto(photo);
            }
        }
        else
        {
            unitOfWork.PhotoRepository.RemovePhoto(photo);
        }

        await unitOfWork.Complete();

        return Unit.Value;
    }
}