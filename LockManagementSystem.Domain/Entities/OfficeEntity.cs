namespace LockManagementSystem.Domain.Entities;

public class OfficeEntity : Entity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Country { get; set; }

    public string State { get; set; }
    
    public string Address { get; set; }

    public int NumberOfDoors { get; set; }

    public int NumberOfLocks { get; set; }

    public ICollection<RoleEntity> Roles { get; set; }

    public ICollection<LockEntity> Locks { get; set; }
}