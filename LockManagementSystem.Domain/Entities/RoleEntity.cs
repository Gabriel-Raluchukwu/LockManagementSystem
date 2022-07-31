namespace LockManagementSystem.Domain.Entities;

public class RoleEntity : Entity
{
    public Guid OfficeId { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }
    
    public OfficeEntity Office { get; set; }
    
    public List<EmployeeRoleEntity> EmployeeRoles { get; set; }
    
    public List<LockRoleEntity> LockRoles { get; set; }
}