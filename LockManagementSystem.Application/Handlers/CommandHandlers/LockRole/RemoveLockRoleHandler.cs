using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.LockRole;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.LockRole;

public class RemoveLockRoleHandler : IRequestHandler<RemoveLockRoleCommand, ResponseModel<RemoveLockFromRoleResponse>>
{
    private readonly IReadRepository<LockRoleEntity> _lockRoleReadRepository;
    private readonly IWriteRepository<LockRoleEntity> _lockRoleWriteRepository;
    
    public RemoveLockRoleHandler(IWriteRepository<LockRoleEntity> lockRoleWriteRepository, IReadRepository<LockRoleEntity> lockRoleReadRepository)
    {
        _lockRoleWriteRepository = lockRoleWriteRepository;
        _lockRoleReadRepository = lockRoleReadRepository;
    }
    
    public async Task<ResponseModel<RemoveLockFromRoleResponse>> Handle(RemoveLockRoleCommand command, CancellationToken cancellationToken)
    {
        var lockRole = await _lockRoleReadRepository.GetByAsync(p => p.LockId == command.LockId && p.RoleId == command.RoleId
            && !p.IsDeprecated);
        if (lockRole is null)
        {
            throw new NotFoundException("Role lock mapping not found.");
        }
        
        _lockRoleWriteRepository.SoftDelete(lockRole);

        var status = await _lockRoleWriteRepository.SaveChangesAsync(cancellationToken) > 0;
        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }
        
        return new ResponseModel<RemoveLockFromRoleResponse>
        {
            Message = "Lock removed from role successfully.",
            Data = LockMapper.Mapper.Map<RemoveLockFromRoleResponse>(lockRole)
        };
    }
}