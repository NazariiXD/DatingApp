using DatingApp.Application.DTOs.Users;
using DatingApp.Application.Interfaces;
using DatingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Features.Auth.Login;

public class LoginCommandHandler(
    UserManager<AppUser> userManager,
    ITokenService tokenService)
    : IRequestHandler<LoginCommand, UserDto>
{
    public async Task<UserDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginDto = request.Login;

        var user = await userManager.Users
            .Include(p => p.Photos)
            .FirstOrDefaultAsync(
                x => x.NormalizedUserName == loginDto.Username.ToUpper(),
                cancellationToken);

        if (user == null || user.UserName == null)
        {
            throw new UnauthorizedAccessException("Invalid username");
        }

        var token = await tokenService.CreateToken(user);

        return new UserDto
        {
            Username = user.UserName,
            KnownAs = user.KnownAs,
            Token = token,
            Gender = user.Gender,
            PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
        };
    }
}