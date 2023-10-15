using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArquitecture.Application.Contracts.Persistence;

namespace CleanArchitecture.Infrastructure.Repositories;

public class StreamerRepository : Repository<Streamer>, IStreamerRepository
{
    public StreamerRepository(StreamerDbContext dbContext) : base(dbContext) { }
}
