namespace LockManagementSystem.Domain.Entities;

public class EmployeeRoleEntity : BaseEntity
{
    public Guid EmployeeId { get; set; }

    public Guid RoleId { get; set; }

    public EmployeeEntity Employee { get; set; }

    public RoleEntity Role { get; set; }
}