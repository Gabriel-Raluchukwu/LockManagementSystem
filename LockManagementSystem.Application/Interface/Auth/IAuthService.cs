using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Interface.Auth;

public interface IAuthService
{
    public Task<bool> HasAccess(Guid employeeId, List<string> roles);
    
    public Task<bool> HasLockAccess(Guid employeeId, Guid lockId);
    
    public Task<EmployeeEntity> SaveEmployeeWithRole(EmployeeEntity employee, Guid? roleId);
}