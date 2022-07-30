using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Enums;

namespace LockManagementSystem.Application.Models.Queries.EventLog;

public class GetEventLogsQuery : BasePagedQuery, IRequest<ResponseModel<PagedResponse<EventLogResponse>>>
{
    public Guid? UserId { get; set; }
    
    public Guid? OfficeId { get; set; }
    
    public Guid? LockId { get; set; }
    
    public DateTime? Start { get; set; }
    
    public DateTime? End { get; set; }

    public LockEventTypeEnum? Type { get; set; }
    
    public LockEventStatusEnum? Status { get; set; }
}