using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Queries.EventLog;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.QueryHandlers.EventLog;

public class GetEventLogByIdHandler : IRequestHandler<GetEventLogByIdQuery, ResponseModel<EventLogResponse>>
{
    private readonly IReadRepository<EventLogEntity> _readRepository;
    
    public GetEventLogByIdHandler(IReadRepository<EventLogEntity> readRepository)
    {
        _readRepository = readRepository;
    }

    public async Task<ResponseModel<EventLogResponse>> Handle(GetEventLogByIdQuery query, CancellationToken cancellationToken)
    {
        var eventLog = await _readRepository.GetByAsync(p => p.Id == query.Id);

        if (eventLog is null)
        {
            throw new NotFoundException("Event log not found.");
        }

        return new ResponseModel<EventLogResponse>
        {
            Message = "Event log retrieved successfully",
            Data = LockMapper.Mapper.Map<EventLogResponse>(eventLog)
        };
    }
}