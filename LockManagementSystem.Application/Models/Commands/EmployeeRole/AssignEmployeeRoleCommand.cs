using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.EmployeeRole;

public class AssignEmployeeRoleCommand :  IRequest<ResponseModel<AssignEmployeeToRoleResponse>>
{
    public Guid EmployeeId { get; set; }

    public Guid RoleId { get; set; }
}

public class AssignEmployeeRoleCommandValidator : AbstractValidator<AssignEmployeeRoleCommand>
{
    public AssignEmployeeRoleCommandValidator()
    {
        RuleFor(x => x.EmployeeId).NotEmpty();
        RuleFor(x => x.RoleId).NotEmpty();
    }
}