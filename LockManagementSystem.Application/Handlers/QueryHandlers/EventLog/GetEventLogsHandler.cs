using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface.EventLog;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Queries.EventLog;
using LockManagementSystem.Application.Models.Responses;

namespace LockManagementSystem.Application.Handlers.QueryHandlers.EventLog;

public class GetEventLogsHandler : IRequestHandler<GetEventLogsQuery, ResponseModel<PagedResponse<EventLogResponse>>>
{
    private readonly IEventLogReadRepository _eventLogReadRepository;
    
    public GetEventLogsHandler(IEventLogReadRepository eventLogReadRepository)
    {
        _eventLogReadRepository = eventLogReadRepository;
    }

    public async Task<ResponseModel<PagedResponse<EventLogResponse>>> Handle(GetEventLogsQuery query, CancellationToken cancellationToken)
    {
        if (query.Start.HasValue && query.End.HasValue && query.Start.Value > query.End.Value)
        {
            throw new BadRequestException("StartDate cannot be greater than EndDate");
        }

        var logs = await _eventLogReadRepository.GetLogs(query, cancellationToken);

        return new ResponseModel<PagedResponse<EventLogResponse>>
        {
            Message = "Logs retrieved successfully.",
            Data = new PagedResponse<EventLogResponse>
            {
                Count = logs.Count,
                PageNumber = logs.PageNumber,
                PageSize = logs.PageSize,
                TotalPages = logs.TotalPages,
                Data = LockMapper.Mapper.Map<List<EventLogResponse>>(logs.Data)
            }
        };
    }
}