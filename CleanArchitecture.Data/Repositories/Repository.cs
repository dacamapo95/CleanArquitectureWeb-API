using CleanArchitecture.Domain.Common;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArquitecture.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : BaseDomainModel
{
    protected readonly StreamerDbContext _dbContext;

    public Repository(StreamerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> AddAsync(T entity)
    {
        _dbContext.Set<T>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public void AddEntity(T entity)
    {
        _dbContext.Set<T>().Add(entity);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.Set<T>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public void UpdateEntity(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.Set<T>().Update(entity);
    }

    public async Task DeleteAsync(T entity)
    {
        _dbContext.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public void DeleteEntity(T entity)
    {
        _dbContext.Remove(entity);
    }

    public async Task<T> GetByIdAsync(int id) => await _dbContext.Set<T>().FindAsync(id);

    public async Task<IReadOnlyList<T>> GetAllAsync() => await _dbContext.Set<T>().ToListAsync();

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate) => await _dbContext.Set<T>().Where(predicate).ToListAsync();

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                 string includeString = null,
                                                 bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (disableTracking) query.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);
        if (orderBy != null) query = orderBy(query);

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                                 Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                                 List<Expression<Func<T, object>>> includes = null,
                                                 string includeString = null,
                                                 bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (disableTracking) query.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);
        if (orderBy != null) query = orderBy(query);
        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));
        if (orderBy != null) query = orderBy(query);

        return await query.ToListAsync();
    }


}
