using FluentValidation;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Enums;

namespace LockManagementSystem.Application.Models.Commands.EventLog;

public class CreateEventLogCommand : IRequest<ResponseModel<CreateEventLogResponse>>
{
    public Guid UserId { get; set; }

    public Guid OfficeId { get; set; }

    public Guid LockId { get; set; }
    
    public LockEventTypeEnum Type { get; set; }

    public LockEventStatusEnum Status { get; set; }
    
    public DateTime OccurredAt { get; set; }
}

public class CreateEventLogCommandValidator : AbstractValidator<CreateEventLogCommand>
{
    public CreateEventLogCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.OfficeId).NotEmpty();
        RuleFor(x => x.LockId).NotEmpty();
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.Status).IsInEnum();
        RuleFor(x => x.OccurredAt).NotEmpty().WithMessage("{PropertyName} is required")
            .Must(dateTime => dateTime <= DateTime.UtcNow).WithMessage("{PropertyName} is not valid");
    }
}