using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.Lock;

public class CreateLockCommand : IRequest<ResponseModel<CreateLockResponse>>
{
    public Guid OfficeId { get; set; }

    public string Location { get; set; }

    public string Model { get; set; }
    
    public string SerialNo { get; set; }

    public DateTime DateInstalled { get; set; }
}

public class CreateLockCommandValidator : AbstractValidator<CreateLockCommand>
{
    public CreateLockCommandValidator()
    {
        RuleFor(x => x.OfficeId).NotEmpty();
        RuleFor(x => x.SerialNo).NotEmpty();
    }
}