using CleanArchitecture.Domain;

namespace CleanArquitecture.Application.Contracts.Persistence
{
    public interface IVideoRepository : IRepository<Video>
    {
        Task<Video> GetVideoByName(string name);

        Task<IEnumerable<Video>> GetVideosByUserName(string userName);
    }
}
