namespace LockManagementSystem.Application.Models.Queries;

public class BaseQuery
{
    public Guid Id { get; set; }
}

public class BasePagedQuery
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}