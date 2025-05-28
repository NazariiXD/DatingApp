using DatingApp.Application.Repositories;
using MediatR;

namespace DatingApp.Application.Features.Likes.GetUserLikeIds;

public class GetCurrentUserLikeIdsQueryHandler(
    IUnitOfWork unitOfWork)
    : IRequestHandler<GetCurrentUserLikeIdsQuery, IEnumerable<int>>
{
    public async Task<IEnumerable<int>> Handle(GetCurrentUserLikeIdsQuery request, CancellationToken cancellationToken)
    {
        var likeIds = await unitOfWork.LikesRepository.GetCurrentUserLikeIds(request.UserId);

        return likeIds;
    }
}