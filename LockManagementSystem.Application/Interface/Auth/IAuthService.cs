using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Interface.Auth;

public interface IAuthService
{
    public Task<EmployeeEntity> SaveEmployeeWithRole(EmployeeEntity employee, Guid? roleId);
}