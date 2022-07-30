using LockManagementSystem.Application.Attributes;
using LockManagementSystem.Application.Models.Commands.EventLog;
using LockManagementSystem.Application.Models.Queries.EventLog;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Enums;
using MediatR;

namespace LockManagementSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class EventLogController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public EventLogController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Post log
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [LogEventAuth]
    [HttpPost("log")]
    public async Task<ActionResult<ResponseModel<CreateEventLogResponse>>> LogEvent(CreateEventLogCommand command)
    {
        var validationResult = await new CreateEventLogCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Get log event
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResponseModel<EventLogResponse>>> GetLogEvent(Guid id)
    {
        var query = new GetEventLogByIdQuery {Id = id};
        var validationResult = await new GetEventLogByIdValidator().ValidateAsync(query);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(query);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Query event logs
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="officeId"></param>
    /// <param name="lockId"></param>
    /// <param name="type"></param>
    /// <param name="status"></param>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("events")]
    public async Task<ActionResult<ResponseModel<PagedResponse<EventLogResponse>>>> GetLogEvent([FromQuery] Guid? userId, Guid? officeId, Guid? lockId, LockEventTypeEnum? type,
        LockEventStatusEnum? status, DateTime? start, DateTime? end, int? pageNumber, int? pageSize)
    {
        var query = new GetEventLogsQuery
        {
            UserId = userId,
            OfficeId = officeId,
            LockId = lockId,
            Type = type,
            Status = status,
            Start = start,
            End = end,
            PageNumber = pageNumber ?? 1,
            PageSize = pageSize ?? 10
        };
        return await _mediator.Send(query);
    }
}