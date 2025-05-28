using AutoMapper;
using DatingApp.Application.DTOs.Users;
using DatingApp.Application.Interfaces;
using DatingApp.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.Application.Features.Auth.Register;

public class RegisterCommandHandler(
    UserManager<AppUser> userManager,
    ITokenService tokenService,
    IMapper mapper)
    : IRequestHandler<RegisterCommand, UserDto>
{
    public async Task<UserDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var registerDto = request.Register;

        if (await UserExists(registerDto.Username, cancellationToken))
        {
            throw new ArgumentException("Username is taken");
        }

        var user = mapper.Map<AppUser>(registerDto);

        user.UserName = registerDto.Username.ToLower();

        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            throw new InvalidOperationException(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        var token = await tokenService.CreateToken(user);

        return new UserDto
        {
            Username = user.UserName,
            Token = token,
            KnownAs = user.KnownAs,
            Gender = user.Gender
        };
    }

    private Task<bool> UserExists(string username, CancellationToken cancellationToken)
    {
        return userManager.Users.AnyAsync(
            x => x.NormalizedUserName == username.ToUpper(),
            cancellationToken);
    }
}