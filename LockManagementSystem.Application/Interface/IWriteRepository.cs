using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Interface;

public interface IWriteRepository<T> where T : BaseEntity
{
    public void Insert(T entity);

    public void Delete(T entity);

    public void SoftDelete(T entity);

    public void Update(T entity);
    
    public Task SaveChangesAsync();

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}