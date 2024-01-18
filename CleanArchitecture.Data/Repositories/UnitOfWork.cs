using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArquitecture.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CleanArchitecture.Infrastructure.Repositories
{
    internal class UnitOfWork : IUnitOfWork
    {
        private Hashtable _repositories;
        private readonly StreamerDbContext _context;

        public UnitOfWork(StreamerDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
        {
            if (_repositories == null) _repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                Type repositoryType = typeof(IRepository<>);
                var respositoryIntance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);
                _repositories.Add(type, respositoryIntance);
            }

            return (IRepository<TEntity>)_repositories[type]!;
        }

        public Task<int> SaveTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
