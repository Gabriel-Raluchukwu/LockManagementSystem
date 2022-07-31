using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.EmployeeRole;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.EmployeeRole;

public class RemoveEmployeeRoleHandler : IRequestHandler<RemoveEmployeeRoleCommand, ResponseModel<RemoveEmployeeFromRoleResponse>>
{
    private readonly IReadRepository<EmployeeRoleEntity> _employeeRoleReadRepository;
    private readonly IWriteRepository<EmployeeRoleEntity> _employeeRoleWriteRepository;

    public RemoveEmployeeRoleHandler(IWriteRepository<EmployeeRoleEntity> employeeRoleWriteRepository, IReadRepository<EmployeeRoleEntity> employeeRoleReadRepository)
    {
        _employeeRoleWriteRepository = employeeRoleWriteRepository;
        _employeeRoleReadRepository = employeeRoleReadRepository;
    }

    public async Task<ResponseModel<RemoveEmployeeFromRoleResponse>> Handle(RemoveEmployeeRoleCommand command, CancellationToken cancellationToken)
    {
        var employeeRole = await _employeeRoleReadRepository.GetByAsync(p => p.EmployeeId == command.EmployeeId && p.RoleId == command.RoleId
            && !p.IsDeprecated);
        if (employeeRole is null)
        {
            throw new NotFoundException("Role employee mapping not found.");
        }
        
        _employeeRoleWriteRepository.SoftDelete(employeeRole);

        var status = await _employeeRoleWriteRepository.SaveChangesAsync(cancellationToken) > 0;
        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }
        
        return new ResponseModel<RemoveEmployeeFromRoleResponse>
        {
            Message = "Employee removed from role successfully.",
            Data = LockMapper.Mapper.Map<RemoveEmployeeFromRoleResponse>(employeeRole)
        };
    }
}