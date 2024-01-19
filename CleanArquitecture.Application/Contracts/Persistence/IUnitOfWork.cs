using CleanArchitecture.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArquitecture.Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel;

    Task<int> SaveTransaction();

    IVideoRepository VideoRepository { get; }

    IStreamerRepository StreamerRepository { get; }
}
