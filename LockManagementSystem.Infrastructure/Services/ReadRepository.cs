using System.Linq.Expressions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Domain.Entities;
using LockManagementSystem.Domain.Models;
using LockManagementSystem.Infrastructure.Extensions;
using LockManagementSystem.Infrastructure.Persistence;

namespace LockManagementSystem.Infrastructure.Services;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> _dbSet;

    public ReadRepository(LockManagementReadContext dbContext)
    {
        _dbSet = dbContext.Set<T>();
    }
    
    public T GetById(Guid id)
    {
        return _dbSet.Find(id);
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<List<T>> GetMultiple(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public Task<PagedModel<T>> GetPagedMultiple<TKey>(int pageNumber, int pageSize, Expression<Func<T, TKey>> orderPredicate)
    {
        return _dbSet.AsNoTracking().OrderBy(orderPredicate).ToPagedResultAsync(pageNumber, pageSize);
    }

    public Task<PagedModel<T>> GetPagedMultiple<TKey>(int pageNumber, int pageSize, Expression<Func<T, bool>> searchPredicate, Expression<Func<T, TKey>> orderPredicate)
    {
        var query = _dbSet.AsNoTracking().Where(searchPredicate);
        return query.OrderBy(orderPredicate).ToPagedResultAsync(pageNumber, pageSize);
    }

    public T GetBy(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.FirstOrDefault(predicate);
    }

    public Task<T> GetByAsync(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.FirstOrDefaultAsync(predicate);
    }
}