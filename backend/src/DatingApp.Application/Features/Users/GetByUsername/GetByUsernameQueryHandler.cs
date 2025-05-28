using DatingApp.Application.DTOs.Users;
using DatingApp.Application.Repositories;
using DatingApp.Domain.Exceptions;
using MediatR;

namespace DatingApp.Application.Features.Users.GetByUsername;

public class GetByUsernameQueryHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetByUsernameQuery, MemberDto>
{
    public async Task<MemberDto> Handle(GetByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetMemberAsync(request.Username, request.CurrentUsername == request.Username);

        if (user is null)
        {
            throw new NotFoundException($"User with username '{request.Username}' not found.");
        }

        return user;
    }
}