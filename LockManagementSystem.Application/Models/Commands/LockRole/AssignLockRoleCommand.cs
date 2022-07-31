using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.LockRole;

public class AssignLockRoleCommand : IRequest<ResponseModel<AssignLockToRoleResponse>>
{
    public Guid LockId { get; set; }

    public Guid RoleId { get; set; }
}

public class AssignLockRoleCommandValidator : AbstractValidator<AssignLockRoleCommand>
{
    public AssignLockRoleCommandValidator()
    {
        RuleFor(x => x.LockId).NotEmpty();
        RuleFor(x => x.RoleId).NotEmpty();
    }
}