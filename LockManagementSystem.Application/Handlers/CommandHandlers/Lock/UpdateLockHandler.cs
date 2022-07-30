using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.Lock;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.Lock;

public class UpdateLockHandler : IRequestHandler<UpdateLockCommand, ResponseModel<UpdateLockResponse>>
{
    private readonly IReadRepository<OfficeEntity> _officeReadRepository;
    private readonly IWriteRepository<LockEntity> _lockWriteRepository;
    private readonly IReadRepository<LockEntity> _lockReadRepository;
    
    public UpdateLockHandler(IReadRepository<OfficeEntity> officeReadRepository, IWriteRepository<LockEntity> lockWriteRepository, IReadRepository<LockEntity> lockReadRepository)
    {
        _officeReadRepository = officeReadRepository;
        _lockWriteRepository = lockWriteRepository;
        _lockReadRepository = lockReadRepository;
    }
    
    public async Task<ResponseModel<UpdateLockResponse>> Handle(UpdateLockCommand command, CancellationToken cancellationToken)
    {
        var lockEntity = await _lockReadRepository.GetByAsync(p => p.Id == command.Id && !p.IsDeprecated);
        if (lockEntity is null)
        {
            throw new NotFoundException("Lock not found.");
        }
        
        var office = await _officeReadRepository.GetByAsync(p => p.Id == command.OfficeId && !p.IsDeprecated);
        if (office is null)
        {
            throw new NotFoundException("Office not found.");
        }

        lockEntity = UpdateLock(lockEntity, command);
        _lockWriteRepository.Update(lockEntity);

        var status = await _lockWriteRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }

        return new ResponseModel<UpdateLockResponse>
        {
            Message = "Lock saved successfully.",
            Data = LockMapper.Mapper.Map<UpdateLockResponse>(lockEntity)
        };
    }
    
    private LockEntity UpdateLock(LockEntity entity, UpdateLockCommand command)
    {
        entity.OfficeId = command.OfficeId;
        entity.Location = command.Location;
        entity.Model = command.Model;
        entity.SerialNo = command.SerialNo;
        entity.DateInstalled = command.DateInstalled;
        entity.UpdatedAt = DateTime.UtcNow;
        return entity;
    }
}