using DatingApp.Application.DTOs;
using DatingApp.Application.DTOs.Users;
using DatingApp.Application.Repositories;
using MediatR;

namespace DatingApp.Application.Features.Users.GetAll;

public class GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetAllUsersQuery, PagedList<MemberDto>>
{
    public async Task<PagedList<MemberDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.UserRepository.GetMembersAsync(request.UserParams);

        return users;
    }
}