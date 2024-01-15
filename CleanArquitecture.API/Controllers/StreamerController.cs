using CleanArquitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArquitecture.Application.Features.Streamers.Commands.DeleteStreamer;
using CleanArquitecture.Application.Features.Streamers.Commands.UpdateStreamer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArquitecture.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class StreamerController : Controller
{
    private readonly IMediator _mediator;

    public StreamerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Administrator")]
    [HttpPost(Name = "CreateStreamer")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<ActionResult<int>> CreateStreamer([FromBody] CreateStreamerCommand createStreamerCommand) 
        => await _mediator.Send(createStreamerCommand);

    [HttpPut]
    [ProducesResponseType((int)StatusCodes.Status204NoContent)]
    [ProducesResponseType((int)StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> UpdateStreamer([FromBody] UpdateStreamerCommand updateStreamerCommand)
    {
        await _mediator.Send(updateStreamerCommand);
        return NoContent();
    }

    [HttpDelete("{id}", Name = "DeleteStreamer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeleteStreamer(int id)
        => Ok(await _mediator.Send(new DeleteStreamerCommand(id)));
}
