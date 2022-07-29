using LockManagementSystem.Application.Models.Commands.Employee;
using LockManagementSystem.Application.Models.Queries.Employee;
using LockManagementSystem.Application.Models.Responses;
using MediatR;

namespace LockManagementSystem.Controllers;

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
}