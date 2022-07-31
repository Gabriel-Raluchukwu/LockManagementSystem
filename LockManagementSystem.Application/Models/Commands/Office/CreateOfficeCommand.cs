using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.Office;

public class CreateOfficeCommand : IRequest<ResponseModel<CreateOfficeResponse>>
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Country { get; set; }

    public string State { get; set; }
    
    public string Address { get; set; }

    public int NumberOfDoors { get; set; }

    public int NumberOfLocks { get; set; }
}

public class CreateOfficeCommandValidator : AbstractValidator<CreateOfficeCommand>
{
    public CreateOfficeCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("{PropertyName} is required")
            .Matches("^[A-Za-z -]+$").WithMessage("{PropertyName} cannot contain digits or special characters");
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Country).NotEmpty(). WithMessage("{PropertyName} is required")
            .Matches("^[A-Za-z -]+$").WithMessage("{PropertyName} cannot contain digits or special characters");
        RuleFor(x => x.Address).NotEmpty();
        RuleFor(x => x.NumberOfDoors).NotEmpty();
    }
}