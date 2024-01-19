using CleanArquitecture.Application.Features.Directors.Commands.CreateDirector;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArquitecture.API.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class DirectorController : ControllerBase
{
    private IMediator _mediator;

    public DirectorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost(Name ="CreateDirector")]
#if !DEBUG
    [Authorize(Roles = "Administrator")]
#endif

    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CreateDirector([FromBody] CreateDirectorCommand createDirectorCommand)
        => await _mediator.Send(createDirectorCommand);

}
