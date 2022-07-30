using System.Data;
using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.EventLog;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.EventLog;

public class CreateEventLogHandler : IRequestHandler<CreateEventLogCommand, ResponseModel<CreateEventLogResponse>>
{
    //TODO: User
    //private readonly IReadRepository<U> _officeReadRepository;
    private readonly IReadRepository<OfficeEntity> _officeReadRepository;
    private readonly IReadRepository<LockEntity> _lockReadRepository;
    private readonly IWriteRepository<EventLogEntity> _eventLogWriteRepository;

    public CreateEventLogHandler(IReadRepository<OfficeEntity> officeReadRepository, IReadRepository<LockEntity> lockReadRepository, 
        IWriteRepository<EventLogEntity> eventLogWriteRepository)
    {
        _officeReadRepository = officeReadRepository;
        _lockReadRepository = lockReadRepository;
        _eventLogWriteRepository = eventLogWriteRepository;
    }


    public async Task<ResponseModel<CreateEventLogResponse>> Handle(CreateEventLogCommand command, CancellationToken cancellationToken)
    {
        var office = await _officeReadRepository.GetByAsync(p => p.Id == command.OfficeId && !p.IsDeprecated);
        if (office is null)
        {
            throw new NotFoundException("Office not found.");
        }
        
        var lockEntity = await _lockReadRepository.GetByAsync(p => p.Id == command.LockId && !p.IsDeprecated);
        if (lockEntity is null)
        {
            throw new NotFoundException("Lock not found.");
        }

        if (lockEntity.OfficeId != office.Id)
        {
            throw new DataException("Invalid Mapping.Lock and office do not match");
        }
        
        var entity = LockMapper.Mapper.Map<EventLogEntity>(command);
        
        _eventLogWriteRepository.Insert(entity);

        var status = await _eventLogWriteRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }

        return new ResponseModel<CreateEventLogResponse>
        {
            Message = "Event log saved successfully.",
            Data = LockMapper.Mapper.Map<CreateEventLogResponse>(entity)
        };
    }
}