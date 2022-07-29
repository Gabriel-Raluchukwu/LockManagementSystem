using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Queries.Employee;

public class GetEmployeeDetailQuery : BaseQuery, IRequest<ResponseModel<EmployeeDetailsResponse>>
{
    
}

public class GetEmployeeDetailQueryValidator : AbstractValidator<GetEmployeeDetailQuery>
{
    public GetEmployeeDetailQueryValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}