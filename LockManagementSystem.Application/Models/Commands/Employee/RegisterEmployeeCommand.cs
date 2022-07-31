using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.Employee;

public class RegisterEmployeeCommand : IRequest<ResponseModel<EmployeeDetailsResponse>>
{
    public Guid OfficeId { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string MiddleName { get; set; }

    public string Gender { get; set; }

    public DateTime EmploymentDate { get; set; }

    public string PhoneNumber { get; set; }

    public string Nationality { get; set; }
    
    public DateTime DateOfBirth { get; set; }

    public string Address { get; set; }
    
    public string State { get; set; }
    
    public string Country { get; set; }
}

public class RegisterEmployeeCommandValidator : AbstractValidator<RegisterEmployeeCommand>
{
    public RegisterEmployeeCommandValidator()
    {
        RuleFor(x => x.OfficeId).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("{PropertyName} is required")
            .Matches("^[A-Za-z -]+$").WithMessage("{PropertyName} cannot contain digits or special characters");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("{PropertyName} is required")
            .Matches("^[A-Za-z -]+$").WithMessage("{PropertyName} cannot contain digits or special characters");
        RuleFor(x => x.Gender).NotEmpty().WithMessage("{PropertyName} is required")
            .Matches("^[A-Za-z -]+$").WithMessage("{PropertyName} cannot contain digits or special characters");
        RuleFor(x => x.EmploymentDate).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("{PropertyName} is required")
            .Matches("^[0-9]+$").WithMessage("{PropertyName} can only contain digits");
        RuleFor(x => x.Nationality).NotEmpty().WithMessage("{PropertyName} is required")
            .Matches("^[A-Za-z -]+$").WithMessage("{PropertyName} cannot contain digits or special characters");
        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage("{PropertyName} is required")
            .Must(dateTime => dateTime <= DateTime.UtcNow).WithMessage("{PropertyName} is not valid");;
        RuleFor(x => x.Country).NotEmpty().WithMessage("{PropertyName} is required")
            .Matches("^[A-Za-z -]+$").WithMessage("{PropertyName} cannot contain digits or special characters");

        RuleFor(x => x.MiddleName)
            .Matches("^[A-Za-z -]+$").WithMessage("{PropertyName} cannot contain digits or special characters")
            .When(x => !string.IsNullOrWhiteSpace(x.MiddleName));
        RuleFor(x => x.Email).EmailAddress().WithMessage("{PropertyName} is not valid")
            .When(x => !string.IsNullOrWhiteSpace(x.Email));
    }
}