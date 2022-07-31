using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Queries.LockRole;

public class GetLockRolesQuery : IRequest<ResponseModel<List<LockRoleResponse>>>
{
    public Guid LockId { get; set; }
}

public class GetLockRolesQueryValidator : AbstractValidator<GetLockRolesQuery>
{
    public GetLockRolesQueryValidator()
    {
        RuleFor(x => x.LockId).NotEmpty();
    }
}