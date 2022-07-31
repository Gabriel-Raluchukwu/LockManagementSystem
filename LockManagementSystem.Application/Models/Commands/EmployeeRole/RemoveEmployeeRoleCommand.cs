using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.EmployeeRole;

public class RemoveEmployeeRoleCommand : IRequest<ResponseModel<RemoveEmployeeFromRoleResponse>>
{
    public Guid EmployeeId { get; set; }

    public Guid RoleId { get; set; }
}

public class RemoveEmployeeRoleCommandValidator : AbstractValidator<RemoveEmployeeRoleCommand>
{
    public RemoveEmployeeRoleCommandValidator()
    {
        RuleFor(x => x.EmployeeId).NotEmpty();
        RuleFor(x => x.RoleId).NotEmpty();
    }
} 