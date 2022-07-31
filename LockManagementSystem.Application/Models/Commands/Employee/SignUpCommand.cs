using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.Employee;

public class SignUpCommand: IRequest<ResponseModel<SignUpResponse>>
{
    public Guid EmployeeDetailId { get; set; }
    
    public string Password { get; set; }
}

public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
{
    public SignUpCommandValidator()
    {
        RuleFor(x => x.EmployeeDetailId).NotEmpty();
        RuleFor(x => x.Password)
            .MinimumLength(6).WithMessage("Password must be at least 6 characters")
            .Matches("\\d").WithMessage("Password must contain at least one digit")
            .Must(password => password.Any(c => !char.IsLetterOrDigit(c))).WithMessage("{PropertyName} must contain a special character");
    }
}