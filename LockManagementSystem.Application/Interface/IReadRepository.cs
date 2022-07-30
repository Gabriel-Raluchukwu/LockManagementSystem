using System.Linq.Expressions;
using LockManagementSystem.Domain.Entities;
using LockManagementSystem.Domain.Models;

namespace LockManagementSystem.Application.Interface;

public interface IReadRepository<T> where T : BaseEntity
{
    public T GetById(Guid id);

    public Task<T> GetByIdAsync(Guid id);

    public Task<List<T>> GetMultiple(Expression<Func<T, bool>> predicate);

    public Task<PagedModel<T>> GetPagedMultiple<TKey>(int pageNumber, int pageSize, Expression<Func<T, TKey>> orderPredicate);
    
    public Task<PagedModel<T>> GetPagedMultiple<TKey>(int pageNumber, int pageSize, Expression<Func<T, bool>> searchPredicate, 
        Expression<Func<T, TKey>> orderPredicate);
    
    public T? GetBy(Expression<Func<T, bool>> predicate);

    public Task<T?> GetByAsync(Expression<Func<T, bool>> predicate);
}