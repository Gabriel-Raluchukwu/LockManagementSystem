using LockManagementSystem.Application.Interface.Auth;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;
using LockManagementSystem.Infrastructure.Persistence;

namespace LockManagementSystem.Infrastructure.Services.Auth;

#nullable disable warnings
public class AuthService : IAuthService
{
    private readonly LockManagementReadContext _dbContext;
    
    public AuthService(LockManagementReadContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<EmployeeEntity> SaveEmployeeWithRole(EmployeeEntity employee, Guid? roleId)
    {
        var role = await GetRole(roleId);
        if (role == null)
        {
            return null;
        }

        using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        {
            await _dbContext.Employees.AddAsync(employee);

            var employeeRole = new EmployeeRoleEntity
            {
                EmployeeId = employee.Id,
                RoleId = role.Id
            };

            await _dbContext.EmployeeRoles.AddAsync(employeeRole);

            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
        }

        return employee;
    }

    private async Task<RoleEntity> GetRole(Guid? roleId)
    {
        if (roleId is null || roleId == Guid.Empty)
        {
            // Use default 'Employee' role
            var roleName = Constants.DefaultRole.ToLower();
            return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == roleName);
        }

        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
    }
}