using LockManagementSystem.Application.Models.Commands.Office;
using LockManagementSystem.Application.Models.Queries.Office;
using LockManagementSystem.Application.Models.Responses;
using MediatR;

namespace LockManagementSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class OfficeController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public OfficeController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Create an office
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<ResponseModel<CreateOfficeResponse>>> CreateOffice(CreateOfficeCommand command)
    {
        var validationResult = await new CreateOfficeCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Update an office
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<ActionResult<ResponseModel<UpdateOfficeResponse>>> UpdateOffice(UpdateOfficeCommand command)
    {
        var validationResult = await new UpdateOfficeCommandValidator().ValidateAsync(command);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(command);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }
    
    /// <summary>
    /// Get an office by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ResponseModel<OfficeResponse>>> GetOffice(Guid id)
    {
        var query = new GetOfficeByIdQuery { Id = id };
        var validationResult = await new GetOfficeByIdQueryValidator().ValidateAsync(query);
        if (validationResult.IsValid)
        {
            return await _mediator.Send(query);
        }
        return BadRequest(validationResult.Errors.Select(p => p.ErrorMessage).ToList());
    }

    /// <summary>
    /// Get offices
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <returns></returns>
    // TODO: Authorize Endpoint (Administrator role)
    [HttpGet("paged")]
    public async Task<ActionResult<ResponseModel<PagedResponse<OfficeResponse>>>> GetOffices([FromQuery]int? pageSize, int? pageNumber)
    {
        var query = new GetOfficesQuery {PageSize = pageSize ?? 10, PageNumber = pageNumber ?? 1};
        return await _mediator.Send(query);
    }
}