using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.Roles;

public class CreateRoleCommand : IRequest<ResponseModel<CreateRoleResponse>>
{
    public Guid OfficeId { get; set; }
    
    public string Name { get; set; }

    public string Description { get; set; }
}

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(x => x.OfficeId).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}