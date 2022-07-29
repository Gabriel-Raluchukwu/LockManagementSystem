using System.Linq.Expressions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Domain.Entities;
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

    public List<T> GetMultiple(Expression<Func<T, bool>> predicate)
    {
        throw new NotImplementedException();
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