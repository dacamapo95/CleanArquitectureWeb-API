using CleanArquitecture.Application.Features.Videos.Queries.GetVideos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CleanArquitecture.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class VideoController : ControllerBase
{
    private readonly IMediator _mediator;

    public VideoController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [Authorize]
    [HttpGet("{username}", Name = "GetVideo")]
    [ProducesResponseType(typeof(IEnumerable<VideoVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<VideoVm>>> GetVideosByUsername(string username) 
        => Ok(await _mediator.Send(new GetVideosQuery(username)));
} 
