using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Models.Commands.Lock;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;
using LockManagementSystem.Domain.Enums;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.Lock;

public class OpenLockHandler : IRequestHandler<OpenLockCommand, ResponseModel<OpenLockResponse>>
{
    private readonly IReadRepository<LockEntity> _lockReadRepository;
    private readonly IReadRepository<EmployeeEntity> _employeeReadRepository;
    private readonly IReadRepository<EmployeeDetailEntity> _employeeDetailReadRepository;
    private readonly IWriteRepository<EventLogEntity> _eventLogWriteRepository;
    
    public OpenLockHandler(IReadRepository<LockEntity> lockReadRepository, IReadRepository<EmployeeEntity> employeeReadRepository, 
        IReadRepository<EmployeeDetailEntity> employeeDetailReadRepository, IWriteRepository<EventLogEntity> eventLogWriteRepository)
    {
        _lockReadRepository = lockReadRepository;
        _employeeReadRepository = employeeReadRepository;
        _employeeDetailReadRepository = employeeDetailReadRepository;
        _eventLogWriteRepository = eventLogWriteRepository;
    }
    
    public async Task<ResponseModel<OpenLockResponse>> Handle(OpenLockCommand command, CancellationToken cancellationToken)
    {
        var employee = await _employeeReadRepository.GetByAsync(p => p.Id == command.EmployeeId && !p.IsDeprecated);
        if (employee is null)
        {
            var logEvent = CreateEventLog(command.LockId, Guid.Empty, command.EmployeeId, LockEventTypeEnum.Access, LockEventStatusEnum.Unauthorized, DateTime.UtcNow);
            await LogEvent(logEvent, cancellationToken);
            throw new NotFoundException("Employee not found.");
        }
            
        var lockEntity = await _lockReadRepository.GetByAsync(p => p.Id == command.LockId && !p.IsDeprecated);
        if (lockEntity is null)
        {
            var logEvent = CreateEventLog(command.LockId, Guid.Empty, command.EmployeeId, LockEventTypeEnum.Access, LockEventStatusEnum.Error, DateTime.UtcNow);
            await LogEvent(logEvent, cancellationToken);
            throw new NotFoundException("Lock not found.");
        }
        
        var employeeDetail = await _employeeDetailReadRepository.GetByAsync(p => p.Id == command.EmployeeId && !p.IsDeprecated);
        if (employeeDetail is null)
        {
            var logEvent = CreateEventLog(command.LockId, Guid.Empty, command.EmployeeId, LockEventTypeEnum.Access, LockEventStatusEnum.Unauthorized, DateTime.UtcNow);
            await LogEvent(logEvent, cancellationToken);
            throw new NotFoundException("Employee detail not found.");
        }

        if (employeeDetail.OfficeId != lockEntity.OfficeId)
        {
            throw new BadRequestException("Wrong office.");
        }

        //External Api call to trigger door lock
        var status = SendLockEvent();

        var lockStatus = status ? LockEventStatusEnum.Opened : LockEventStatusEnum.Error;
        var _logEvent = CreateEventLog(command.LockId, lockEntity.OfficeId, command.EmployeeId, LockEventTypeEnum.Access, lockStatus, DateTime.UtcNow);
        await LogEvent(_logEvent, cancellationToken);

        return new ResponseModel<OpenLockResponse>
        {
            Message = "Processed lock event.",
            Data = new OpenLockResponse
            {
                Model = lockEntity.Model,
                SerialNo = lockEntity.SerialNo,
                Status = lockStatus.ToString()
            }
        };
    }

    private bool SendLockEvent()
    {
        Thread.Sleep(500);
        return new Random(DateTime.UtcNow.Millisecond).Next(1,100) % 2 == 0;
    }
    

    private async Task<EventLogEntity> LogEvent(EventLogEntity eventLog, CancellationToken cancellationToken)
    {
        _eventLogWriteRepository.Insert(eventLog);
        var status = await _eventLogWriteRepository.SaveChangesAsync(cancellationToken) > 0;
        return status ? eventLog : null;
    }
    
    private static EventLogEntity CreateEventLog(Guid lockId, Guid officeId, Guid employeeId, LockEventTypeEnum type, LockEventStatusEnum status, DateTime? occurredAt = default)
    {
        return new EventLogEntity
        {
            LockId = lockId,
            OfficeId = officeId,
            UserId = employeeId,
            Type = type,
            Status = status,
            OccurredAt = occurredAt ?? DateTime.UtcNow
        };
    }
}