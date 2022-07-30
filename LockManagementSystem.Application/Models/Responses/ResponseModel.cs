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

public class PagedResponse<T>
{
    public int PageSize { get; set; }
    
    public int PageNumber { get; set; }
    
    public int Count { get; set; }
    
    public int TotalPages { get; set; }
    
    public List<T> Data { get; set; }
}
