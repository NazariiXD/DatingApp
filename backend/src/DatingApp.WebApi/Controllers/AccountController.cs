using DatingApp.Application.DTOs.Auth;
using DatingApp.Application.DTOs.Users;
using DatingApp.Application.Features.Auth.Login;
using DatingApp.Application.Features.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers;

public class AccountController : BaseApiController
{
    public AccountController(IMediator mediator)
        : base(mediator)
    {
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        return Ok(await Mediator.Send(new RegisterCommand(registerDto)));
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        return Ok(await Mediator.Send(new LoginCommand(loginDto)));
    }
}
