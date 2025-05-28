using DatingApp.Application.DTOs;
using DatingApp.Application.DTOs.Users;
using MediatR;

namespace DatingApp.Application.Features.Users.GetAll;

public record GetAllUsersQuery(UserParams UserParams)
    : IRequest<PagedList<MemberDto>>;