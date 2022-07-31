namespace LockManagementSystem.Application.Models.Responses;

public class RoleResponse
{
    public Guid OfficeId { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }
}

public class CreateRoleResponse : RoleResponse
{
    
}

public class UpdateRoleResponse : RoleResponse
{
    
}