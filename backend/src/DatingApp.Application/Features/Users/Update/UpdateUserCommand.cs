using DatingApp.Application.DTOs.Users;
using MediatR;

namespace DatingApp.Application.Features.Users.Update;

public record UpdateUserCommand(MemberUpdateDto MemberUpdate, string Username)
    : IRequest<Unit>;