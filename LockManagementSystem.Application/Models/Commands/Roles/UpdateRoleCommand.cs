using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.Roles;

public class UpdateRoleCommand : IRequest<ResponseModel<UpdateRoleResponse>>
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }
}

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}