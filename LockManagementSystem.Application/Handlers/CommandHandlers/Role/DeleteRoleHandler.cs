using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.Roles;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.Role;

public class DeleteRoleHandler : IRequestHandler<DeleteRoleCommand, ResponseModel<bool>>
{
    private readonly IReadRepository<RoleEntity> _roleReadRepository;
    private readonly IWriteRepository<RoleEntity> _roleWriteRepository;
    
    public DeleteRoleHandler(IWriteRepository<RoleEntity> roleWriteRepository, IReadRepository<RoleEntity> roleReadRepository)
    {
        _roleReadRepository = roleReadRepository;
        _roleWriteRepository = roleWriteRepository;
    }
    
    public async Task<ResponseModel<bool>> Handle(DeleteRoleCommand command, CancellationToken cancellationToken)
    {
        var role = await _roleReadRepository.GetByAsync(p => p.Id == command.Id && !p.IsDeprecated);
        if (role is null)
        {
            throw new NotFoundException("Role not found.");
        }
        
        _roleWriteRepository.SoftDelete(role);
        // Or completely delete using '_roleWriteRepository.Delete(role)'
        
        var status = await _roleWriteRepository.SaveChangesAsync(cancellationToken) > 0;
        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }

        return new ResponseModel<bool>
        {
            Message = "Role deleted successfully.",
            Data = status
        };
    }
}