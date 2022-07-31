namespace LockManagementSystem.Domain.Entities;

public class LockEntity : Entity
{
    public Guid OfficeId { get; set; }

    public string Location { get; set; }

    public string Model { get; set; }
    
    public string SerialNo { get; set; }

    public DateTime DateInstalled { get; set; }
    
    public OfficeEntity Office { get; set; }
    
    public List<LockRoleEntity> LockRoles { get; set; }
}