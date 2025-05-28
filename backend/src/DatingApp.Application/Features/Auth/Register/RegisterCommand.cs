using DatingApp.Application.DTOs.Auth;
using DatingApp.Application.DTOs.Users;
using MediatR;

namespace DatingApp.Application.Features.Auth.Register;

public record RegisterCommand(RegisterDto Register)
    : IRequest<UserDto>;