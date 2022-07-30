using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Queries.Office;

public class GetOfficeByIdQuery : BaseQuery, IRequest<ResponseModel<OfficeResponse>>
{
    
}

public class GetOfficeByIdQueryValidator : AbstractValidator<GetOfficeByIdQuery>
{
    public GetOfficeByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}