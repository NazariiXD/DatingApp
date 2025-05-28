using AutoMapper;
using DatingApp.Application.DTOs.Users.Photos;
using DatingApp.Application.Interfaces;
using DatingApp.Application.Repositories;
using DatingApp.Domain.Entities;
using DatingApp.Domain.Exceptions;
using MediatR;

namespace DatingApp.Application.Features.Users.Photos.AddPhoto;

public class AddPhotoCommandHandler(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IPhotoService photoService)
    : IRequestHandler<AddPhotoCommand, PhotoDto>
{
    public async Task<PhotoDto> Handle(AddPhotoCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetUserByUsernameAsync(request.Username);

        if (user == null)
        {
            throw new NotFoundException("Cannot update user");
        }

        var result = await photoService.AddPhotoAsync(request.File);

        if (result.Error != null)
        {
            throw new InvalidOperationException(result.Error.Message);
        }

        var photo = new Photo
        {
            Url = result.SecureUrl.AbsoluteUri,
            PublicId = result.PublicId
        };

        user.Photos.Add(photo);

        if (!await unitOfWork.Complete())
        {
            throw new InvalidOperationException("Failed to add photo");
        }

        return mapper.Map<PhotoDto>(photo);
    }
}