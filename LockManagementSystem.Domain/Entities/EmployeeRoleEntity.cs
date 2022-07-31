namespace LockManagementSystem.Domain.Entities;

/// <summary>
/// Many to Many table mapping an employee to multiple roles
/// </summary>
public class EmployeeRoleEntity : BaseEntity
{
    public Guid EmployeeId { get; set; }

    public Guid RoleId { get; set; }

    public EmployeeEntity Employee { get; set; }

    public RoleEntity Role { get; set; }
}