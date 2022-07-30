using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Queries.Employee;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.QueryHandlers.Employee;

public class GetEmployeeDetailsByIdHandler : IRequestHandler<GetEmployeeDetailQuery, ResponseModel<EmployeeDetailsResponse>>
{
    private readonly IReadRepository<EmployeeDetailEntity> _readRepository;

    public GetEmployeeDetailsByIdHandler(IReadRepository<EmployeeDetailEntity> readRepository)
    {
        _readRepository = readRepository;
    }
    
    public async Task<ResponseModel<EmployeeDetailsResponse>> Handle(GetEmployeeDetailQuery request, CancellationToken cancellationToken)
    {
        var employeeDetail = await _readRepository.GetByAsync(p => p.Id == request.Id && !p.IsDeprecated);

        if (employeeDetail == null)
        {
            throw new NotFoundException("Employee details not found.");
        }

        return new ResponseModel<EmployeeDetailsResponse>
        {
            Message = "Employee Details retrieved successfully",
            Data = LockMapper.Mapper.Map<EmployeeDetailsResponse>(employeeDetail)
        };
    }
}