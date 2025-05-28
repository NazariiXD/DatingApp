using DatingApp.Application.DTOs.Users;
using DatingApp.Application.DTOs.Users.Photos;
using DatingApp.Application.Extensions;
using DatingApp.Application.Features.Users.GetAll;
using DatingApp.Application.Features.Users.GetByUsername;
using DatingApp.Application.Features.Users.Photos.AddPhoto;
using DatingApp.Application.Features.Users.Photos.DeletePhoto;
using DatingApp.Application.Features.Users.Photos.SetMainPhoto;
using DatingApp.Application.Features.Users.Update;
using DatingApp.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers;

[Authorize]
public class UsersController : BaseApiController
{
    public UsersController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery]UserParams userParams)
    {
        userParams.CurrentUsername = User.GetUsername();

        var users = await Mediator.Send(new GetAllUsersQuery(userParams));

        Response.AddPaginationHeader(users);

        return Ok(users);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        return Ok(await Mediator.Send(new GetByUsernameQuery(username, User.GetUsername())));
    }

    [HttpPut]
    public async Task<ActionResult> UpdateUser(MemberUpdateDto memberUpdateDto)
    {
        await Mediator.Send(new UpdateUserCommand(memberUpdateDto, User.GetUsername()));

        return NoContent();
    }

    [HttpPost("add-photo")]
    public async Task<ActionResult<PhotoDto>> AddPhoto(IFormFile file)
    {
        var username = User.GetUsername();

        var photo = await Mediator.Send(new AddPhotoCommand(file, username));

        return CreatedAtAction(nameof(GetUser), new { username }, photo);
    }

    [HttpPut("set-main-photo/{photoId:int}")]
    public async Task<ActionResult> SetMainPhoto(int photoId)
    {
        await Mediator.Send(new SetMainPhotoCommand(photoId, User.GetUsername()));

        return NoContent();
    }

    [HttpDelete("delete-photo/{photoId:int}")]
    public async Task<ActionResult> DeletePhoto(int photoId)
    {
        await Mediator.Send(new DeletePhotoCommand(photoId, User.GetUsername()));

        return Ok();
    }
}
