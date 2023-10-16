using CleanArchitecture.Domain;
using CleanArquitecture.Application.Features.Videos.Queries.GetVideos;

namespace CleanArquitecture.Application.Contracts.Persistence
{
    public interface IVideoRepository : IRepository<Video>
    {
        Task<Video> GetVideoByName(string name);

        Task<IEnumerable<Video>> GetVideosByUserName(string userName);

        Task<List<VideoVm>> GetVideosWithSelect();
    }
}
