using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Queries.Roles;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.QueryHandlers.Role;

public class GetRolesHandler : IRequestHandler<GetRolesQuery, ResponseModel<PagedResponse<RoleResponse>>>
{
    private readonly IReadRepository<RoleEntity> _readRepository;
    
    public GetRolesHandler(IReadRepository<RoleEntity> readRepository)
    {
        _readRepository = readRepository;
    }
    
    public async Task<ResponseModel<PagedResponse<RoleResponse>>> Handle(GetRolesQuery query, CancellationToken cancellationToken)
    {
        var pagedResult = await _readRepository.GetPagedMultiple(query.PageNumber, query.PageSize, 
            p => p.OfficeId == query.OfficeId && !p.IsDeprecated,p => p.Name);
        
        return new ResponseModel<PagedResponse<RoleResponse>>
        {
            Message = "Roles retrieved successfully",
            Data = new PagedResponse<RoleResponse>
            {
                Count = pagedResult.Count,
                PageNumber = pagedResult.PageNumber,
                PageSize = pagedResult.PageSize,
                TotalPages = pagedResult.TotalPages,
                Data = LockMapper.Mapper.Map<List<RoleResponse>>(pagedResult.Data)
            }
        };
    }
}