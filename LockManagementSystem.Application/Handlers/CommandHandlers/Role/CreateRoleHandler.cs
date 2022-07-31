using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.Roles;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.Role;

public class CreateRoleHandler : IRequestHandler<CreateRoleCommand, ResponseModel<CreateRoleResponse>>
{
    private readonly IReadRepository<OfficeEntity> _officeReadRepository;
    private readonly IReadRepository<RoleEntity> _roleReadRepository;
    private readonly IWriteRepository<RoleEntity> _roleWriteRepository;

    public CreateRoleHandler(IReadRepository<OfficeEntity> officeReadRepository, IWriteRepository<RoleEntity> roleWriteRepository, 
        IReadRepository<RoleEntity> roleReadRepository)
    {
        _officeReadRepository = officeReadRepository;
        _roleWriteRepository = roleWriteRepository;
        _roleReadRepository = roleReadRepository;
    }
    
    public async Task<ResponseModel<CreateRoleResponse>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        var office = await _officeReadRepository.GetByAsync(p => p.Id == command.OfficeId && !p.IsDeprecated);
        if (office is null)
        {
            throw new NotFoundException("Office not found.");
        }
        
        var duplicateRole = await _roleReadRepository.GetByAsync(p => p.OfficeId == command.OfficeId && p.Name.ToLower() == command.Name.ToLower()
            && !p.IsDeprecated);
        if (duplicateRole is not null)
        {
            throw new BadRequestException("Role already exists.");
        }

        var role = LockMapper.Mapper.Map<RoleEntity>(command);
        
        _roleWriteRepository.Insert(role);
        
        var status = await _roleWriteRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }
        
        return new ResponseModel<CreateRoleResponse>
        {
            Message = "Role saved successfully.",
            Data = LockMapper.Mapper.Map<CreateRoleResponse>(role)
        };
    }
}