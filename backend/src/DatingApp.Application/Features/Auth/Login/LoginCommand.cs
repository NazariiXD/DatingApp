using DatingApp.Application.DTOs.Auth;
using DatingApp.Application.DTOs.Users;
using MediatR;

namespace DatingApp.Application.Features.Auth.Login;

public record LoginCommand(LoginDto Login)
    : IRequest<UserDto>;