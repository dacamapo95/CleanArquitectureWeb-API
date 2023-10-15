using CleanArquitecture.Application.Features.Videos.Queries.GetVideos;
using MediatR;
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

    [HttpGet("{username}", Name = "GetVideo")]
    [ProducesResponseType(typeof(IEnumerable<VideoVm>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<IEnumerable<VideoVm>>> GetVideosByUsername(string username)
    {
        var query = new GetVideosQuery(username);
        var videos = await _mediator.Send(query);
        return Ok(videos);
    }



} 
