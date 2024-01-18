using CleanArchitecture.Domain.Common;
using System.Linq.Expressions;

namespace CleanArquitecture.Application.Contracts.Persistence;

public interface IRepository<T> where T : BaseDomainModel
{
    Task<T> AddAsync(T entity);

    Task<T> UpdateAsync(T entity);

    Task<T> GetByIdAsync(int id);

    Task DeleteAsync(T entity);

    Task<IReadOnlyList<T>> GetAllAsync();

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                    string includeString = null,
                                    bool disableTracking = true);

    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                    List<Expression<Func<T, object>>> includes = null,
                                    string includeString = null,
                                    bool disableTracking = true);

    void AddEntity(T entity);
    void UpdateEntity(T entity);
    void DeleteEntity(T entity);
}
