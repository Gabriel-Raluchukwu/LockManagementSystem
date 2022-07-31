using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Queries.LockRole;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.QueryHandlers.LockRole;

public class GetLockRolesHandler : IRequestHandler<GetLockRolesQuery, ResponseModel<List<LockRoleResponse>>>
{
    private readonly IReadRepository<LockRoleEntity> _lockRoleReadRepository;
    private readonly IReadRepository<RoleEntity> _roleReadRepository;
    
    public GetLockRolesHandler(IReadRepository<LockRoleEntity> lockRoleReadRepository, IReadRepository<RoleEntity> roleReadRepository)
    {
        _lockRoleReadRepository = lockRoleReadRepository;
        _roleReadRepository = roleReadRepository;
    }
    
    public async Task<ResponseModel<List<LockRoleResponse>>> Handle(GetLockRolesQuery query, CancellationToken cancellationToken)
    {
        var lockRoleEntities =
            await _lockRoleReadRepository.GetMultiple(p => p.LockId == query.LockId && !p.IsDeprecated);

        var roleIds = lockRoleEntities.Select(p => p.RoleId).Distinct().ToList();
        if (!roleIds.Any())
        {
            return new ResponseModel<List<LockRoleResponse>>
            {
                Message = "Lock roles retrieved successfully.",
                Data = new List<LockRoleResponse>()
            };
        }

        var roles = await _roleReadRepository.GetMultiple(p => roleIds.Contains(p.Id) && !p.IsDeprecated);
        
        return new ResponseModel<List<LockRoleResponse>>
        {
            Message = "Lock roles retrieved successfully.",
            Data = LockMapper.Mapper.Map<List<LockRoleResponse>>(roles)
        };
    }
}