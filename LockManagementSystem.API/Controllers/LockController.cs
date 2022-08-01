using LockManagementSystem.Application.Interface.Auth;
using LockManagementSystem.Application.Models.Commands.Lock;
using LockManagementSystem.Application.Models.Commands.LockRole;
using LockManagementSystem.Application.Models.Queries.Lock;
using LockManagementSystem.Application.Models.Queries.LockRole;
using LockManagementSystem.Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LockManagementSystem.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class LockController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly IAuthService _authService;
    
    public LockController(IMediator mediator, IAuthService authService)
    {
        _mediator = mediator;
        _authService = authService;
    }
    
    /// <summary>
    /// Create a lock
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ResponseModel<CreateLockResponse>>> CreateLock(CreateLockCommand command)
    {
        var validationResult = await new CreateLockCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Update lock
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<ResponseModel<UpdateLockResponse>>> UpdateLock(UpdateLockCommand command)
    {
        var validationResult = await new UpdateLockCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Get lock
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResponseModel<LockResponse>>> GetLockById(Guid id)
    {
        var query = new GetLockByIdQuery {Id = id};
        var validationResult = await new GetLockByIdQueryValidator().ValidateAsync(query);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(query);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Get locks
    /// </summary>
    /// <param name="officeId"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet("{officeId:guid}/office")]
    public async Task<ActionResult<ResponseModel<PagedResponse<LockResponse>>>> GetOfficeLock([FromRoute]Guid officeId, [FromQuery] int? pageSize, int? pageNumber)
    {
        var query = new GetLocksQuery {OfficeId = officeId, PageNumber = pageNumber ?? 1, PageSize = pageSize ?? 10};
        var validationResult = await new GetLocksQueryValidator().ValidateAsync(query);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(query);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Open lock
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("open")]
    public async Task<ActionResult<ResponseModel<OpenLockResponse>>> OpenLock(OpenLockCommand command)
    {
        if (!await _authService.HasLockAccess(command.EmployeeId, command.LockId))
        {
            return Unauthorized(new ResponseModel<OpenLockResponse>());
        }
        
        var validationResult = await new OpenLockCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Get lock roles
    /// </summary>
    /// <param name="lockId"></param>
    /// <returns></returns>
    [HttpGet("role/{lockId:guid}")]
    public async Task<ActionResult<ResponseModel<List<LockRoleResponse>>>> GetLockRoles(Guid lockId)
    {
        
        var query = new GetLockRolesQuery {LockId = lockId};
        var validationResult = await new GetLockRolesQueryValidator().ValidateAsync(query);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(query);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Assign role to lock
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("role/save")]
    public async Task<ActionResult<ResponseModel<AssignLockToRoleResponse>>> AddLockRole(AssignLockRoleCommand command)
    {
        var validationResult = await new AssignLockRoleCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Remove role from lock
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("role/delete")]
    public async Task<ActionResult<ResponseModel<RemoveLockFromRoleResponse>>> RemoveLockRole(RemoveLockRoleCommand command)
    {
        var validationResult = await new RemoveLockRoleCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
}
