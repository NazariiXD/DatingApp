using DatingApp.Application.Repositories;
using DatingApp.Domain.Entities;
using MediatR;

namespace DatingApp.Application.Features.Likes.ToggleLike;

public class ToggleLikeCommandHandler(
    IUnitOfWork unitOfWork) 
    : IRequestHandler<ToggleLikeCommand, Unit>
{
    public async Task<Unit> Handle(ToggleLikeCommand request, CancellationToken cancellationToken)
    {
        var targetUserId = request.TargetUserId;
        var sourceUserId = request.SourceUserId;

        if (sourceUserId == targetUserId)
        {
            throw new ArgumentException("You cannot like yourself");
        }

        var existingLike = await unitOfWork.LikesRepository.GetUserLike(sourceUserId, targetUserId);

        if (existingLike == null)
        {
            var like = new UserLike
            {
                SourceUserId = sourceUserId,
                TargetUserId = targetUserId
            };

            unitOfWork.LikesRepository.AddLike(like);
        }
        else
        {
            unitOfWork.LikesRepository.DeleteLike(existingLike);
        }

        if (!await unitOfWork.Complete())
        {
            throw new InvalidOperationException("Failed to update like");
        }

        return Unit.Value;
    }
}