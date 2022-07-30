using LockManagementSystem.Application.Interface.EventLog;
using LockManagementSystem.Application.Models.Queries.EventLog;
using LockManagementSystem.Domain.Entities;
using LockManagementSystem.Domain.Enums;
using LockManagementSystem.Domain.Models;
using LockManagementSystem.Infrastructure.Extensions;
using LockManagementSystem.Infrastructure.Persistence;

namespace LockManagementSystem.Infrastructure.Services.Repositories.EventLog;

public class EventLogReadRepository : IEventLogReadRepository
{
    private readonly DbSet<EventLogEntity> _dbSet;
    
    public EventLogReadRepository(LockManagementReadContext dbContext)
    {
        _dbSet = dbContext.Set<EventLogEntity>();
    }
    
    public Task<PagedModel<EventLogEntity>> GetLogs(GetEventLogsQuery queryParams, CancellationToken cancellationToken = default)
    {
        var endDate = queryParams.End ?? DateTime.UtcNow;
        var startDate = queryParams.Start ?? endDate.AddDays(-7);

        var logQuery = _dbSet.Where(p => (p.OccurredAt >= startDate && p.OccurredAt <= endDate)
                                     || (p.CreatedAt >= startDate && p.CreatedAt <= endDate));

        if (queryParams.UserId.HasValue && queryParams.UserId != Guid.Empty)
        {
            logQuery = logQuery.Where(p => p.UserId == queryParams.UserId.Value);
        }
        
        if (queryParams.OfficeId.HasValue && queryParams.OfficeId != Guid.Empty)
        {
            logQuery = logQuery.Where(p => p.OfficeId == queryParams.OfficeId.Value);
        }
        
        if (queryParams.LockId.HasValue && queryParams.LockId != Guid.Empty)
        {
            logQuery = logQuery.Where(p => p.LockId == queryParams.LockId);
        }
        
        if (queryParams.Type.HasValue && Enum.IsDefined(typeof(LockEventTypeEnum), queryParams.Type))
        {
            logQuery = logQuery.Where(p => p.Type == queryParams.Type);
        }
        
        if (queryParams.Status.HasValue && Enum.IsDefined(typeof(LockEventStatusEnum), queryParams.Status))
        {
            logQuery = logQuery.Where(p => p.Status == queryParams.Status);
        }

        return logQuery.ToPagedResultAsync(queryParams.PageNumber, queryParams.PageSize);
    }
}