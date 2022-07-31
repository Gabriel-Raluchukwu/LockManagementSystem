using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Queries.Roles;

public class GetRolesQuery : BasePagedQuery, IRequest<ResponseModel<PagedResponse<RoleResponse>>>
{
    public Guid OfficeId { get; set; }
}