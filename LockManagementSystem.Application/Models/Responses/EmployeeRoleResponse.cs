namespace LockManagementSystem.Application.Models.Responses;

public class EmployeeRoleResponse : BaseResponse
{
    public string Name { get; set; }

    public string Description { get; set; }
}

public class AssignEmployeeToRoleResponse 
{
    public Guid EmployeeId { get; set; }

    public Guid RoleId { get; set; }
}

public class RemoveEmployeeFromRoleResponse 
{
    public Guid EmployeeId { get; set; }

    public Guid RoleId { get; set; }
}