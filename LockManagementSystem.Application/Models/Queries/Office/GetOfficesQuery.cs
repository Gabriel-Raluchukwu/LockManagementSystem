using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Queries.Office;

public class GetOfficesQuery : BasePagedQuery, IRequest<ResponseModel<PagedResponse<OfficeResponse>>>
{
    
}