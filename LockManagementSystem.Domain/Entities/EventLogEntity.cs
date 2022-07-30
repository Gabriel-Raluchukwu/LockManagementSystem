using LockManagementSystem.Domain.Enums;

namespace LockManagementSystem.Domain.Entities;

public class EventLogEntity : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid OfficeId { get; set; }

    public Guid LockId { get; set; }
    
    public LockEventTypeEnum Type { get; set; }

    public LockEventStatusEnum Status { get; set; }

    public DateTime OccurredAt { get; set; }
}