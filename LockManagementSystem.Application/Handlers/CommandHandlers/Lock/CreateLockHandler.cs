using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.Lock;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.Lock;

public class CreateLockHandler : IRequestHandler<CreateLockCommand, ResponseModel<CreateLockResponse>>
{
    private readonly IReadRepository<OfficeEntity> _officeReadRepository;
    private readonly IWriteRepository<LockEntity> _lockWriteRepository;
    
    public CreateLockHandler(IReadRepository<OfficeEntity> officeReadRepository, IWriteRepository<LockEntity> lockWriteRepository)
    {
        _officeReadRepository = officeReadRepository;
        _lockWriteRepository = lockWriteRepository;
    }
    
    public async Task<ResponseModel<CreateLockResponse>> Handle(CreateLockCommand command, CancellationToken cancellationToken)
    {
        var office = await _officeReadRepository.GetByAsync(p => p.Id == command.OfficeId && !p.IsDeprecated);
        if (office is null)
        {
            throw new NotFoundException("Office not found.");
        }
        
        var entity = LockMapper.Mapper.Map<LockEntity>(command);
        
        _lockWriteRepository.Insert(entity);

        var status = await _lockWriteRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }

        return new ResponseModel<CreateLockResponse>
        {
            Message = "Lock saved successfully.",
            Data = LockMapper.Mapper.Map<CreateLockResponse>(entity)
        };
    }
}