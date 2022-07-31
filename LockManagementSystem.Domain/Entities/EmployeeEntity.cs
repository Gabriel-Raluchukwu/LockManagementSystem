namespace LockManagementSystem.Domain.Entities;

public class EmployeeEntity : BaseEntity
{
    public string Email { get; set; }
    
    public string PasswordHash { get; set; }
    
    public EmployeeDetailEntity Detail { get; set; }
    
    public List<EmployeeRoleEntity> EmployeeRoles { get; set; }
}