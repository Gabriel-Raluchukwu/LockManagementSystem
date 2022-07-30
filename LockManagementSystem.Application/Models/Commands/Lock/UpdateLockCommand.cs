using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.Lock;

public class UpdateLockCommand : IRequest<ResponseModel<UpdateLockResponse>>
{
    public Guid Id { get; set; }
    
    public Guid OfficeId { get; set; }

    public string Location { get; set; }

    public string Model { get; set; }
    
    public string SerialNo { get; set; }
    
    public DateTime DateInstalled { get; set; }
}

public class UpdateLockCommandValidator : AbstractValidator<UpdateLockCommand>
{
    public UpdateLockCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.OfficeId).NotEmpty();
    }
}