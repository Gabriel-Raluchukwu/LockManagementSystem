using LockManagementSystem.Application.Models.Commands.Roles;
using LockManagementSystem.Application.Models.Queries.Roles;
using LockManagementSystem.Application.Models.Responses;
using MediatR;

namespace LockManagementSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Create role
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ResponseModel<CreateRoleResponse>>> CreateRole(CreateRoleCommand command)
    {
        var validationResult = await new CreateRoleCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Update role
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<ResponseModel<UpdateRoleResponse>>> UpdateRole(UpdateRoleCommand command)
    {
        var validationResult = await new UpdateRoleCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Get roles
    /// </summary>
    /// <param name="officeId"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("{officeId:guid}/role")]
    public async Task<ActionResult<ResponseModel<PagedResponse<RoleResponse>>>> GetRoles(Guid officeId, [FromQuery] int? pageNumber, int? pageSize)
    {
        var query = new GetRolesQuery {OfficeId = officeId, PageNumber = pageNumber ?? 1, PageSize = pageSize ?? 20};
        return await _mediator.Send(query);
    }
    
    /// <summary>
    /// Delete role
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ResponseModel<bool>>> UpdateRole([FromRoute] Guid id)
    {
        var command = new DeleteRoleCommand {Id = id};
        var validationResult = await new DeleteRoleCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
}