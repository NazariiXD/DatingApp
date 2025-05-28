using DatingApp.Application.Repositories;
using DatingApp.Domain.Exceptions;
using MediatR;

namespace DatingApp.Application.Features.Users.Photos.SetMainPhoto;

public class SetMainPhotoCommandHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<SetMainPhotoCommand, Unit>
{
    public async Task<Unit> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(request.Username);

        if (user == null)
        {
            throw new NotFoundException("Could not find user");
        }

        var photo = user.Photos.FirstOrDefault(x => x.Id == request.PhotoId);

        if (photo == null || photo.IsMain)
        {
            throw new ArgumentException("This photo cannot be set as main photo");
        }

        var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);
        if (currentMain != null) currentMain.IsMain = false;
        photo.IsMain = true;

        if (!await unitOfWork.Complete())
        {
            throw new InvalidOperationException("Failed to set main photo");
        }

        return Unit.Value;
    }
}