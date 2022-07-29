namespace LockManagementSystem.Application.Models.Responses;

public class EmployeeDetailsResponse : BaseResponse
{
    public Guid OfficeId { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string MiddleName { get; set; }

    public string Gender { get; set; }

    public DateTime EmploymentDate { get; set; }

    public string PhoneNumber { get; set; }

    public string Nationality { get; set; }
    
    public DateTime DateOfBirth { get; set; }

    public string Address { get; set; }
    
    public string State { get; set; }
    
    public string Country { get; set; }
}