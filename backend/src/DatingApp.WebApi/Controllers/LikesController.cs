using DatingApp.Application.DTOs.Likes;
using DatingApp.Application.DTOs.Users;
using DatingApp.Application.Extensions;
using DatingApp.Application.Features.Likes.GetUserLikeIds;
using DatingApp.Application.Features.Likes.GetUserLikes;
using DatingApp.Application.Features.Likes.ToggleLike;
using DatingApp.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers;

public class LikesController : BaseApiController
{
    public LikesController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpPost("{targetUserId:int}")]
    public async Task<ActionResult> ToggleLike(int targetUserId)
    {
        await Mediator.Send(new ToggleLikeCommand(targetUserId, User.GetUserId()));

        return Ok();
    }

    [HttpGet("list")]
    public async Task<ActionResult<IEnumerable<int>>> GetCurrentUserLikeIds()
    {
        return Ok(await Mediator.Send(new GetCurrentUserLikeIdsQuery(User.GetUserId())));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUserLikes([FromQuery]LikesParams likesParams)
    {
        likesParams.UserId = User.GetUserId();

        var users = await Mediator.Send(new GetUserLikesQuery(likesParams));

        Response.AddPaginationHeader(users);

        return Ok(users);
    }
}
