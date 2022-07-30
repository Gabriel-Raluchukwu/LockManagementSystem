using LockManagementSystem.Application.Models.Queries.EventLog;
using LockManagementSystem.Domain.Entities;
using LockManagementSystem.Domain.Models;

namespace LockManagementSystem.Application.Interface.EventLog;

public interface IEventLogReadRepository
{
    Task<PagedModel<EventLogEntity>> GetLogs(GetEventLogsQuery queryParams, CancellationToken cancellationToken = default);
}