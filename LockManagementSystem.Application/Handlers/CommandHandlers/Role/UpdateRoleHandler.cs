using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.Roles;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.Role;

public class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand, ResponseModel<UpdateRoleResponse>>
{
    private readonly IReadRepository<RoleEntity> _roleReadRepository;
    private readonly IWriteRepository<RoleEntity> _roleWriteRepository;
    
    public UpdateRoleHandler(IWriteRepository<RoleEntity> roleWriteRepository, IReadRepository<RoleEntity> roleReadRepository)
    {
        _roleWriteRepository = roleWriteRepository;
        _roleReadRepository = roleReadRepository;
    }
    
    public async Task<ResponseModel<UpdateRoleResponse>> Handle(UpdateRoleCommand command, CancellationToken cancellationToken)
    {
        var role = await _roleReadRepository.GetByAsync(p => p.Id == command.Id && !p.IsDeprecated);
        if (role is null)
        {
            throw new NotFoundException("Role not found.");
        }

        role = UpdateRole(role, command);
        _roleWriteRepository.Update(role);

        var status = await _roleWriteRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }

        return new ResponseModel<UpdateRoleResponse>
        {
            Message = "Role updated successfully.",
            Data = LockMapper.Mapper.Map<UpdateRoleResponse>(role)
        };
    }

    private RoleEntity UpdateRole(RoleEntity entity, UpdateRoleCommand command)
    {
        entity.Name = command.Name;
        entity.Description = command.Description;
        return entity;
    }
}