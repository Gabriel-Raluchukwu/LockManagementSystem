namespace LockManagementSystem.Application.Models.Responses;

public class OfficeResponse : BaseResponse
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Country { get; set; }

    public string State { get; set; }
    
    public string Address { get; set; }

    public int NumberOfDoors { get; set; }

    public int NumberOfLocks { get; set; }
}

public class CreateOfficeResponse : OfficeResponse
{
    
}

public class UpdateOfficeResponse : OfficeResponse
{
    
}