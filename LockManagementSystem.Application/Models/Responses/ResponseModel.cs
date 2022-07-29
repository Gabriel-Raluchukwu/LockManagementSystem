namespace LockManagementSystem.Application.Models.Responses;

public class ResponseModel<T>
{
    public string Message { get; set; }

    public T Data { get; set; }
}

public class BaseResponse
{
    public Guid Id { get; set; }
}