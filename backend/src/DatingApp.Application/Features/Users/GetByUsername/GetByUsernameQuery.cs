using DatingApp.Application.DTOs.Users;
using MediatR;

namespace DatingApp.Application.Features.Users.GetByUsername;

public record GetByUsernameQuery(string Username, string CurrentUsername)
    : IRequest<MemberDto>;