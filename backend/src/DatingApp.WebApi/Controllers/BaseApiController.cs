using DatingApp.WebApi.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.WebApi.Controllers;

[ServiceFilter(typeof(LogUserActivity))]
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected readonly IMediator Mediator;

    public BaseApiController(IMediator mediator)
    {
        Mediator = mediator;
    }
}
