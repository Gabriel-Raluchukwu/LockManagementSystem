using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.EmployeeRole;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.EmployeeRole;

public class AssignEmployeeRoleHandler : IRequestHandler<AssignEmployeeRoleCommand, ResponseModel<AssignEmployeeToRoleResponse>>
{
    private readonly IReadRepository<EmployeeEntity> _employeeReadRepository; 
    private readonly IReadRepository<RoleEntity> _roleReadRepository;
    private readonly IReadRepository<EmployeeRoleEntity> _employeeRoleReadRepository;
    private readonly IWriteRepository<EmployeeRoleEntity> _employeeRoleWriteRepository;
    
    public AssignEmployeeRoleHandler(IReadRepository<EmployeeEntity> employeeReadRepository, IReadRepository<RoleEntity> roleReadRepository,
        IWriteRepository<EmployeeRoleEntity> employeeRoleWriteRepository, IReadRepository<EmployeeRoleEntity> employeeRoleReadRepository)
    {
        _employeeReadRepository = employeeReadRepository;
        _roleReadRepository = roleReadRepository;
        _employeeRoleWriteRepository = employeeRoleWriteRepository;
        _employeeRoleReadRepository = employeeRoleReadRepository;
    }
    
    public async Task<ResponseModel<AssignEmployeeToRoleResponse>> Handle(AssignEmployeeRoleCommand command, CancellationToken cancellationToken)
    {
        var employee = await _employeeReadRepository.GetByAsync(p => p.Id == command.EmployeeId && !p.IsDeprecated);
        if (employee is null)
        {
            throw new NotFoundException("Employee not found.");
        }
        
        var role = await _roleReadRepository.GetByAsync(p => p.Id == command.RoleId && !p.IsDeprecated);
        if (role is null)
        {
            throw new NotFoundException("Role not found.");
        }

        var duplicate = await _employeeRoleReadRepository.GetByAsync(p => p.EmployeeId == command.EmployeeId 
                                                    && p.RoleId == command.RoleId && !p.IsDeprecated);

        if (duplicate is not null)
        {
            return new ResponseModel<AssignEmployeeToRoleResponse>
            {
                Message = "Role assigned to employee successfully.",
                Data = LockMapper.Mapper.Map<AssignEmployeeToRoleResponse>(duplicate)
            };
        }

        var employeeRole = LockMapper.Mapper.Map<EmployeeRoleEntity>(command);

        _employeeRoleWriteRepository.Insert(employeeRole);

        var status = await _employeeRoleWriteRepository.SaveChangesAsync(cancellationToken) > 0;
        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }
        
        return new ResponseModel<AssignEmployeeToRoleResponse>
        {
            Message = "Role assigned to employee successfully.",
            Data = LockMapper.Mapper.Map<AssignEmployeeToRoleResponse>(employeeRole)
        };
    }
}