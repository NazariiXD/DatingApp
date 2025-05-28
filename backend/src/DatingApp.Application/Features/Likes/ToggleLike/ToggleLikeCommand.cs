using MediatR;

namespace DatingApp.Application.Features.Likes.ToggleLike;

public record ToggleLikeCommand(int TargetUserId, int SourceUserId)
    : IRequest<Unit>;