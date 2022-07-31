using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.Lock;

public class OpenLockCommand : IRequest<ResponseModel<OpenLockResponse>>
{
    public Guid LockId { get; set; }
    
    public Guid EmployeeId { get; set; }
}

public class OpenLockCommandValidator : AbstractValidator<OpenLockCommand>
{
    public OpenLockCommandValidator()
    {
        RuleFor(x => x.LockId).NotEmpty();
        RuleFor(x => x.EmployeeId).NotEmpty();
    }
}