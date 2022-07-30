using LockManagementSystem.Domain.Entities;
using LockManagementSystem.Domain.Enums;

namespace LockManagementSystem.Application.Interface.EventLog;

public interface IEventLogger
{
    Task<EventLogEntity> LogEvent(Guid userId, Guid officeId, Guid lockId, LockEventTypeEnum type, LockEventStatusEnum status);
}