using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Queries.EmployeeRole;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.QueryHandlers.EmployeeRole;

public class GetEmployeeRolesHandler : IRequestHandler<GetEmployeeRolesQuery, ResponseModel<PagedResponse<EmployeeRoleResponse>>>
{
    private readonly IReadRepository<EmployeeRoleEntity> _employeeRoleReadRepository;
    private readonly IReadRepository<RoleEntity> _roleReadRepository;
    
    public GetEmployeeRolesHandler(IReadRepository<EmployeeRoleEntity> employeeRoleReadRepository, IReadRepository<RoleEntity> roleReadRepository)
    {
        _employeeRoleReadRepository = employeeRoleReadRepository;
        _roleReadRepository = roleReadRepository;
    }

    public async Task<ResponseModel<PagedResponse<EmployeeRoleResponse>>> Handle(GetEmployeeRolesQuery query, CancellationToken cancellationToken)
    {
        var pagedResult =
            await _employeeRoleReadRepository.GetPagedMultiple(query.PageNumber, query.PageSize, p => p.EmployeeId == query.EmployeeId && !p.IsDeprecated, p => p.CreatedAt);

        var roleIds = pagedResult.Data.Select(p => p.RoleId).Distinct().ToList();
        if (!roleIds.Any())
        {
            return new ResponseModel<PagedResponse<EmployeeRoleResponse>>
            {
                Message = "Employee roles retrieved successfully.",
                Data = new PagedResponse<EmployeeRoleResponse>
                {
                    Count = pagedResult.Count,
                    PageNumber = pagedResult.PageNumber,
                    PageSize = pagedResult.PageSize,
                    TotalPages = pagedResult.TotalPages,
                    Data = new List<EmployeeRoleResponse>()
                }
            };
        }

        var rolesPagedResult = await _roleReadRepository.GetPagedMultiple(query.PageNumber, query.PageSize, p => roleIds.Contains(p.Id) && !p.IsDeprecated,
            p => p.Name);
        
        return new ResponseModel<PagedResponse<EmployeeRoleResponse>>
        {
            Message = "Employee roles retrieved successfully.",
            Data = new PagedResponse<EmployeeRoleResponse>
            {
                Count = rolesPagedResult.Count,
                PageNumber = rolesPagedResult.PageNumber,
                PageSize = rolesPagedResult.PageSize,
                TotalPages = rolesPagedResult.TotalPages,
                Data = LockMapper.Mapper.Map<List<EmployeeRoleResponse>>(rolesPagedResult.Data)
            }
        };
    }
}