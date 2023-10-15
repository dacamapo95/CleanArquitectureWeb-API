using AutoMapper;
using CleanArquitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArquitecture.Application.Features.Videos.Queries.GetVideos
{
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
            var videos = await _videoRepository.GetVideoByName(request.UserName);
            return _mapper.Map<List<VideoVm>>(videos);
        }
    }
}
