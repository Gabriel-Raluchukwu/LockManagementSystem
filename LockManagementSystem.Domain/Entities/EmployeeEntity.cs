namespace LockManagementSystem.Domain.Entities;

public class EmployeeEntity : BaseEntity
{
    public string PasswordHash { get; set; }
    
    public EmployeeDetailEntity Detail { get; set; }
}