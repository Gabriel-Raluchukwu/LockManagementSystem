using LockManagementSystem.Application.Attributes;
using LockManagementSystem.Application.Interface.Auth;
using LockManagementSystem.Application.Models.Commands.EventLog;
using LockManagementSystem.Application.Models.Queries.EventLog;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LockManagementSystem.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class EventLogController : ControllerBase
{
    private readonly IMediator _mediator;
    
    private readonly IAuthService _authService;
    
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public EventLogController(IMediator mediator, IAuthService authService, IHttpContextAccessor httpContextAccessor)
    {
        _mediator = mediator;
        _authService = authService;
        _httpContextAccessor = httpContextAccessor;
    }
    
    /// <summary>
    /// Post log
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [AllowAnonymous]
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
        var id = _httpContextAccessor.HttpContext?.User.FindFirst(Constants.EmployeeIdClaim)?.Value ?? string.Empty;
        if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out var employeeId) 
            || !await _authService.HasAccess(employeeId, new List<string>{"Director", "Administrator", "OfficeManager"}))
        {
            return Unauthorized(new ResponseModel<PagedResponse<EventLogResponse>>());
        }
        
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