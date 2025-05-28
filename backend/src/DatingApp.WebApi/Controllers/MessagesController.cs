using DatingApp.Application.DTOs.Messages;
using DatingApp.Application.Extensions;
using DatingApp.Application.Features.Messages.Create;
using DatingApp.Application.Features.Messages.Delete;
using DatingApp.Application.Features.Messages.GetMessagesForUser;
using DatingApp.Application.Features.Messages.GetMessageThread;
using DatingApp.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers;

[Authorize]
public class MessagesController : BaseApiController
{
    public MessagesController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpPost]
    public async Task<ActionResult<MessageDto>> CreateMessage(CreateMessageDto createMessageDto)
    {
        return Ok(await Mediator.Send(new CreateMessageCommand(createMessageDto, User.GetUsername())));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessagesForUser(
        [FromQuery]MessageParams messageParams)
    {
        var username = User.GetUsername();
        messageParams.Username = username;

        var messages = await Mediator.Send(new GetMessagesForUserQuery(messageParams, username));

        Response.AddPaginationHeader(messages);

        return messages;
    }

    [HttpGet("thread/{username}")]
    public async Task<ActionResult<IEnumerable<MessageDto>>> GetMessageThread(string username)
    {
        return Ok(await Mediator.Send(new GetMessageThreadQuery(username, User.GetUsername())));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMessage(int id)
    {
        await Mediator.Send(new DeleteMessageCommand(id, User.GetUsername()));

        return Ok();
    }
}
