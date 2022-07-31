using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.LockRole;

public class RemoveLockRoleCommand : IRequest<ResponseModel<RemoveLockFromRoleResponse>>
{
    public Guid LockId { get; set; }

    public Guid RoleId { get; set; }
}

public class RemoveLockRoleCommandValidator : AbstractValidator<RemoveLockRoleCommand>
{
    public RemoveLockRoleCommandValidator()
    {
        RuleFor(x => x.LockId).NotEmpty();
        RuleFor(x => x.RoleId).NotEmpty();
    }
}