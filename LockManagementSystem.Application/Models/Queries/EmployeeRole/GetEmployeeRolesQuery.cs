using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Queries.EmployeeRole;

public class GetEmployeeRolesQuery : BasePagedQuery, IRequest<ResponseModel<PagedResponse<EmployeeRoleResponse>>>
{
    public Guid EmployeeId { get; set; }
}

public class GetEmployeeRolesQueryValidator : AbstractValidator<GetEmployeeRolesQuery>
{
    public GetEmployeeRolesQueryValidator()
    {
        RuleFor(x => x.EmployeeId);
    }
}