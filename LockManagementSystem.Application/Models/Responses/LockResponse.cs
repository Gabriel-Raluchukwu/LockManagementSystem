namespace LockManagementSystem.Application.Models.Responses;

public class LockResponse : BaseResponse
{
    public Guid OfficeId { get; set; }

    public string Location { get; set; }

    public string Model { get; set; }
    
    public string SerialNo { get; set; }

    public DateTime DateInstalled { get; set; }
}

public class CreateLockResponse : LockResponse
{
    
}

public class UpdateLockResponse : LockResponse
{
    
}