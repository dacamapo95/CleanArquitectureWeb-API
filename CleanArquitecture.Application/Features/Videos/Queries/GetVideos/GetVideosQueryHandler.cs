using AutoMapper;
using CleanArquitecture.Application.Contracts.Persistence;
using CleanArquitecture.Application.Exceptions;
using MediatR;
using System.Net.Http.Headers;

namespace CleanArquitecture.Application.Features.Videos.Queries.GetVideos;

public class GetVideosQueryHandler : IRequestHandler<GetVideosQuery, List<VideoVm>>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IMapper _mapper;

    public GetVideosQueryHandler(IVideoRepository videoRepository, IMapper mapper)
    {
        _videoRepository = videoRepository;
        _mapper = mapper;
    }

    public async Task<List<VideoVm>> Handle(GetVideosQuery request, CancellationToken cancellationToken)
    {
        var videos = await _videoRepository.GetVideosByUserName(request.UserName);
        if (videos.Length == 0)
            throw new NotFoundException($"No se encontraron videos creados por {request.UserName}");
        var Ids = await _videoRepository.GetVideosWithSelect();
        return _mapper.Map<List<VideoVm>>(videos);
    }
}
