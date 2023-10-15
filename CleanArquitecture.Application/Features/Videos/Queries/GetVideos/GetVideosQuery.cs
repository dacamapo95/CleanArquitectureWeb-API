using MediatR;

namespace CleanArquitecture.Application.Features.Videos.Queries.GetVideos
{
    public class GetVideosQuery : IRequest<List<VideoVm>>
    {
        public string UserName { get; set; } = string.Empty;

        public GetVideosQuery(string userName)
        {
            UserName = userName ?? throw new ArgumentException(nameof(userName));
        }
    }

}
