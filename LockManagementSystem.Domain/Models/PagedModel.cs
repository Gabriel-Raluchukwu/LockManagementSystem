namespace LockManagementSystem.Domain.Models;

public class PagedModel<T>
{
    public int PageSize { get; set; }
    
    public int PageNumber { get; set; }
    
    public int Count { get; set; }
    
    public int TotalPages { get; set; }
    
    public List<T> Data { get; set; }
}