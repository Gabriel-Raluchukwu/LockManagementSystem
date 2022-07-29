using System.Linq.Expressions;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Interface;

public interface IReadRepository<T> where T : BaseEntity
{
    public T GetById(Guid id);

    public Task<T> GetByIdAsync(Guid id);

    public List<T> GetMultiple(Expression<Func<T, bool>> predicate);

    public T? GetBy(Expression<Func<T, bool>> predicate);

    public Task<T?> GetByAsync(Expression<Func<T, bool>> predicate);
}