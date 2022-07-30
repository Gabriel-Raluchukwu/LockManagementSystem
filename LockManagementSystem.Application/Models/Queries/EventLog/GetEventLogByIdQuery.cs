using FluentValidation;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Models.Queries.EventLog;

public class GetEventLogByIdQuery : BaseQuery, IRequest<ResponseModel<EventLogResponse>>
{
    
}

public class GetEventLogByIdValidator : AbstractValidator<GetEventLogByIdQuery>
{
    public GetEventLogByIdValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}