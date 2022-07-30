using LockManagementSystem.Domain.Models;

namespace LockManagementSystem.Infrastructure.Extensions;

public static class PaginationExtension
{
    public static async Task<PagedModel<T>> ToPagedResultAsync<T>(this IQueryable<T> query, int pageNumber,
        int pageSize)
    {
        pageNumber = pageNumber < 1 ? 1 : pageNumber;
        pageSize = pageSize < 1 ? 1 : pageSize;

        var items = await query.Page(pageNumber, pageSize).ToListAsync();
        var count = await query.CountAsync();
        var totalPages = (int) Math.Ceiling(count/(double)pageSize);

        return new PagedModel<T>
        {
            Count = count,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = totalPages,
            Data = items,
        };
    }

    private static IQueryable<T> Page<T>(this IQueryable<T> query, int pageNumber, int pageSize)
    {
        var rowsToSkip = (pageNumber - 1) * pageSize;
        return query.Skip(rowsToSkip).Take(pageSize);
    }
}