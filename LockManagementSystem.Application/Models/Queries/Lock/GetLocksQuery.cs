using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Queries.Lock;

public class GetLocksQuery : BasePagedQuery, IRequest<ResponseModel<PagedResponse<LockResponse>>>
{
    public Guid OfficeId { get; set; }
}

public class GetLocksQueryValidator : AbstractValidator<GetLocksQuery>
{
    public GetLocksQueryValidator()
    {
        RuleFor(x => x.OfficeId).NotEmpty();
    }
}