using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.LockRole;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.LockRole;

public class AssignLockRoleHandler : IRequestHandler<AssignLockRoleCommand, ResponseModel<AssignLockToRoleResponse>>
{
    private readonly IReadRepository<LockEntity> _lockReadRepository; 
    private readonly IReadRepository<RoleEntity> _roleReadRepository;
    private readonly IReadRepository<LockRoleEntity> _lockRoleReadRepository;
    private readonly IWriteRepository<LockRoleEntity> _lockRoleWriteRepository;
    
    public AssignLockRoleHandler(IReadRepository<LockEntity> lockReadRepository, IReadRepository<RoleEntity> roleReadRepository,
        IWriteRepository<LockRoleEntity> lockRoleWriteRepository, IReadRepository<LockRoleEntity> lockRoleReadRepository)
    {
        _lockReadRepository = lockReadRepository;
        _roleReadRepository = roleReadRepository;
        _lockRoleWriteRepository = lockRoleWriteRepository;
        _lockRoleReadRepository = lockRoleReadRepository;
    }
    
    public async Task<ResponseModel<AssignLockToRoleResponse>> Handle(AssignLockRoleCommand command, CancellationToken cancellationToken)
    {
        var lockEntity = await _lockReadRepository.GetByAsync(p => p.Id == command.LockId && !p.IsDeprecated);
        if (lockEntity is null)
        {
            throw new NotFoundException("Lock not found.");
        }
        
        var role = await _roleReadRepository.GetByAsync(p => p.Id == command.RoleId && !p.IsDeprecated);
        if (role is null)
        {
            throw new NotFoundException("Role not found.");
        }

        var duplicate = await _lockRoleReadRepository.GetByAsync(p => p.LockId == command.LockId 
                                                    && p.RoleId == command.RoleId && !p.IsDeprecated);

        if (duplicate is not null)
        {
            return new ResponseModel<AssignLockToRoleResponse>
            {
                Message = "Role assigned to lock successfully.",
                Data = LockMapper.Mapper.Map<AssignLockToRoleResponse>(duplicate)
            };
        }

        var lockRoleEntity = LockMapper.Mapper.Map<LockRoleEntity>(command);

        _lockRoleWriteRepository.Insert(lockRoleEntity);

        var status = await _lockRoleWriteRepository.SaveChangesAsync(cancellationToken) > 0;
        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }
        
        return new ResponseModel<AssignLockToRoleResponse>
        {
            Message = "Role assigned to lock successfully.",
            Data = LockMapper.Mapper.Map<AssignLockToRoleResponse>(lockRoleEntity)
        };
    }
}