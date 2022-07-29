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
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.Gender).NotEmpty();
        RuleFor(x => x.EmploymentDate).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Nationality).NotEmpty();
        RuleFor(x => x.DateOfBirth).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
    }
}