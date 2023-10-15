using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArquitecture.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

public class VideoRepository : Repository<Video>, IVideoRepository
{
    private readonly StreamerDbContext _dbContext;

    public VideoRepository(StreamerDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Video> GetVideoByName(string name) 
        => await _dbContext.Videos!.FirstOrDefaultAsync(video => video.Name == name);

    public async Task<IEnumerable<Video>> GetVideosByUserName(string userName)
        => await _dbContext.Videos!.Where(video => video.Name == userName).ToListAsync(); 
}
