using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.Office;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.Office;

public class CreateOfficeHandler : IRequestHandler<CreateOfficeCommand, ResponseModel<CreateOfficeResponse>>
{
    private readonly IWriteRepository<OfficeEntity> _writeRepository;
    
    public CreateOfficeHandler(IWriteRepository<OfficeEntity> writeRepository)
    {
        _writeRepository = writeRepository;
    }
    
    public async Task<ResponseModel<CreateOfficeResponse>> Handle(CreateOfficeCommand command, CancellationToken cancellationToken)
    {
        var entity = LockMapper.Mapper.Map<OfficeEntity>(command);
        
        _writeRepository.Insert(entity);

        var status = await _writeRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }

        return new ResponseModel<CreateOfficeResponse>
        {
            Message = "Office saved successfully.",
            Data = LockMapper.Mapper.Map<CreateOfficeResponse>(entity)
        };
    }
}