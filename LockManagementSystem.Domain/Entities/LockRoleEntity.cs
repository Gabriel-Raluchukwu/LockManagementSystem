namespace LockManagementSystem.Domain.Entities;

/// <summary>
/// Many to Many table mapping roles to a particular lock/door
/// </summary>
public class LockRoleEntity : BaseEntity
{
    public Guid LockId { get; set; }

    public Guid RoleId { get; set; }

    public LockEntity Lock { get; set; }

    public RoleEntity Role { get; set; }
}