namespace LockManagementSystem.Application.Models.Responses;

public class LockRoleResponse 
{
    public string Name { get; set; }

    public string Description { get; set; }
}

public class AssignLockToRoleResponse
{
    public Guid LockId { get; set; }

    public Guid RoleId { get; set; }
}

public class RemoveLockFromRoleResponse 
{
    public Guid LockId { get; set; }

    public Guid RoleId { get; set; }
}