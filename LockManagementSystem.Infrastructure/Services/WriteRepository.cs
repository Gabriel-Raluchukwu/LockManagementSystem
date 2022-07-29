using LockManagementSystem.Application.Interface;
using LockManagementSystem.Domain.Entities;
using LockManagementSystem.Infrastructure.Persistence;

namespace LockManagementSystem.Infrastructure.Services;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    private readonly LockManagementWriteContext _databaseContext;

    private readonly DbSet<T> _dbSet;

    public WriteRepository(LockManagementWriteContext databaseContext)
    {
        _databaseContext = databaseContext;
        _dbSet = _databaseContext.Set<T>();
    }
    
    public void Insert(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void SoftDelete(T entity)
    {
        entity.IsDeprecated = true;
        entity.DeprecatedAt = DateTime.UtcNow;
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public async  Task SaveChangesAsync()
    {
        await _databaseContext.SaveChangesAsync();
    }

    public async  Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await _databaseContext.SaveChangesAsync(cancellationToken);
    }
}