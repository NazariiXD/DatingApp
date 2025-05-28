using MediatR;

namespace DatingApp.Application.Features.Likes.GetUserLikeIds;

public record GetCurrentUserLikeIdsQuery(int UserId)
    : IRequest<IEnumerable<int>>;