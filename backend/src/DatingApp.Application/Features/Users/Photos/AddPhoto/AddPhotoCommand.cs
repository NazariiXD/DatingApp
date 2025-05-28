using DatingApp.Application.DTOs.Users.Photos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace DatingApp.Application.Features.Users.Photos.AddPhoto;

public record AddPhotoCommand(IFormFile File, string Username)
    : IRequest<PhotoDto>;