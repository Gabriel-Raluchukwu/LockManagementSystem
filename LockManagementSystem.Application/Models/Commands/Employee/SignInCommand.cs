using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Commands.Employee;

public class SignInCommand : IRequest<ResponseModel<SignInResponse>>
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}

public class SignInCommandValidator : AbstractValidator<SignInCommand>
{
    public SignInCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("{PropertyValue} is not a valid email address");
        RuleFor(x => x.Password).NotEmpty()
            .MinimumLength(6).WithMessage("Password must be at least 6 characters");
    }
}