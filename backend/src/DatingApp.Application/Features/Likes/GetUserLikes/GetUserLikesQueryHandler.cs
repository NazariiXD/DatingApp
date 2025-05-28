using DatingApp.Application.DTOs;
using DatingApp.Application.DTOs.Users;
using DatingApp.Application.Repositories;
using MediatR;

namespace DatingApp.Application.Features.Likes.GetUserLikes;

public class GetUserLikesQueryHandler(
    IUnitOfWork unitOfWork) 
    : IRequestHandler<GetUserLikesQuery, PagedList<MemberDto>>
{
    public async Task<PagedList<MemberDto>> Handle(GetUserLikesQuery request, CancellationToken cancellationToken)
    {
        var users = await unitOfWork.LikesRepository.GetUserLikes(request.LikesParams);

        return users;
    }
}