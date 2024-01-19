using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArquitecture.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CleanArchitecture.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private Dictionary<string, object> _repositoriesByType;
        private readonly StreamerDbContext _context;
        private IVideoRepository _videoRepository;
        private IStreamerRepository _streamerRepository;

        public IVideoRepository VideoRepository => _videoRepository ??= new VideoRepository(_context);
        public IStreamerRepository StreamerRepository  => _streamerRepository ??= new StreamerRepository(_context);


        public UnitOfWork(StreamerDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveTransaction() => await _context.SaveChangesAsync();

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositoriesByType == null) _repositoriesByType = new Dictionary<string, object>();

            var type = typeof(TEntity).Name;

            if (!_repositoriesByType.ContainsKey(type))
            {
                Type repositoryType = typeof(Repository<>);
                var respositoryIntance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositoriesByType.TryAdd(type, respositoryIntance!);
            }

            return (IRepository<TEntity>)_repositoriesByType[type]!;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
