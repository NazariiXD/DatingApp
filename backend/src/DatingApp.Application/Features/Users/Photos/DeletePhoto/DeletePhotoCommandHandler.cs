using DatingApp.Application.Interfaces;
using DatingApp.Application.Repositories;
using DatingApp.Domain.Exceptions;
using MediatR;

namespace DatingApp.Application.Features.Users.Photos.DeletePhoto;

public class DeletePhotoCommandHandler(
    IUnitOfWork unitOfWork,
    IPhotoService photoService)
    : IRequestHandler<DeletePhotoCommand, Unit>
{
    public async Task<Unit> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(request.Username);

        if (user is null)
        {
            throw new NotFoundException("User not found");
        }

        var photo = await unitOfWork.PhotoRepository.GetPhotoById(request.PhotoId);

        if (photo == null || photo.IsMain)
        {
            throw new ArgumentException("This photo cannot be deleted");
        }

        if (photo.PublicId != null)
        {
            var result = await photoService.DeletePhotoAsync(photo.PublicId);

            if (result.Error != null)
            {
                throw new InvalidOperationException(result.Error.Message);
            }
        }

        user.Photos.Remove(photo);

        await unitOfWork.Complete();

        return Unit.Value;
    }
}