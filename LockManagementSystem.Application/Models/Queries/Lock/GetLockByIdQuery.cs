using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Queries.Lock;

public class GetLockByIdQuery : BaseQuery, IRequest<ResponseModel<LockResponse>>
{
    
}

public class GetLockByIdQueryValidator : AbstractValidator<GetLockByIdQuery>
{
    public GetLockByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}