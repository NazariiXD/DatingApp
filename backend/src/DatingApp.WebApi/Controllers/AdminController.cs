using DatingApp.Application.Features.Admin.EditUserRoles;
using DatingApp.Application.Features.Admin.GetUsersWithRoles;
using DatingApp.Application.Features.Admin.Photos.ApprovePhoto;
using DatingApp.Application.Features.Admin.Photos.GetPhotosForModeration;
using DatingApp.Application.Features.Admin.Photos.RejectPhoto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers;

public class AdminController : BaseApiController
{
    public AdminController(IMediator mediator)
        : base(mediator)
    {
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("users-with-roles")]
    public async Task<ActionResult> GetUsersWithRoles()
    {
        return Ok(await Mediator.Send(new GetUsersWithRolesQuery()));
    }

    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("edit-roles/{username}")]
    public async Task<ActionResult> EditRoles(string username, string roles)
    {
        return Ok(await Mediator.Send(new EditUserRolesCommand(username, roles)));
    }

    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpGet("photos-to-moderate")]
    public async Task<ActionResult> GetPhotosForModeration()
    {
        return Ok(await Mediator.Send(new GetPhotosForModerationQuery()));
    }

    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpPost("approve-photo/{photoId}")]
    public async Task<ActionResult> ApprovePhoto(int photoId)
    {
        await Mediator.Send(new ApprovePhotoCommand(photoId));

        return Ok();
    }

    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpPost("reject-photo/{photoId}")]
    public async Task<ActionResult> RejectPhoto(int photoId)
    {
        await Mediator.Send(new RejectPhotoCommand(photoId));

        return Ok();
    }
}
