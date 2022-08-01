using LockManagementSystem.Application.Models.Commands.Employee;
using LockManagementSystem.Application.Models.Commands.EmployeeRole;
using LockManagementSystem.Application.Models.Queries.Employee;
using LockManagementSystem.Application.Models.Queries.EmployeeRole;
using LockManagementSystem.Application.Models.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace LockManagementSystem.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Sign up
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("signup")]
    public async Task<ActionResult<ResponseModel<SignUpResponse>>> SignUp(SignUpCommand command)
    {
        var validationResult = await new SignUpCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Sign in
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost("signin")]
    public async Task<ActionResult<ResponseModel<SignInResponse>>> SignUp(SignInCommand command)
    {
        var validationResult = await new SignInCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Register a new employee
    /// </summary>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult<ResponseModel<EmployeeDetailsResponse>>> RegisterEmployee(RegisterEmployeeCommand command)
    {
        var validationResult = await new RegisterEmployeeCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Get employee details
    /// </summary>
    /// <returns></returns>
    [HttpGet("detail/{id:guid}")]
    public async Task<ActionResult<ResponseModel<EmployeeDetailsResponse>>> GetEmployeeDetail(Guid id)
    {
        var query = new GetEmployeeDetailQuery {Id = id};
        var validationResult = await new GetEmployeeDetailQueryValidator().ValidateAsync(query);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(query);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Get Employee roles
    /// </summary>
    /// <param name="employeeId"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    [HttpGet("role/{employeeId:guid}")]
    public async Task<ActionResult<ResponseModel<PagedResponse<EmployeeRoleResponse>>>> GetEmployeeRoles(Guid employeeId, [FromQuery] int? pageSize, int? pageNumber)
    {
        var query = new GetEmployeeRolesQuery {EmployeeId = employeeId, PageNumber = pageNumber ?? 1, PageSize = pageSize ?? 10};
        var validationResult = await new GetEmployeeRolesQueryValidator().ValidateAsync(query);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(query);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Assign role to employee
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("role/save")]
    public async Task<ActionResult<ResponseModel<AssignEmployeeToRoleResponse>>> AddEmployeeRole(AssignEmployeeRoleCommand command)
    {
        var validationResult = await new AssignEmployeeRoleCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Remove role from employee
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("role/delete")]
    public async Task<ActionResult<ResponseModel<RemoveEmployeeFromRoleResponse>>> RemoveEmployeeRole(RemoveEmployeeRoleCommand command)
    {
        var validationResult = await new RemoveEmployeeRoleCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
}