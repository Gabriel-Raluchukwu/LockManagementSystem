using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.Office;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.Office;

public class UpdateOfficeHandler : IRequestHandler<UpdateOfficeCommand, ResponseModel<UpdateOfficeResponse>>
{
    private readonly IWriteRepository<OfficeEntity> _writeRepository;
    private readonly IReadRepository<OfficeEntity> _readRepository;
    
    public UpdateOfficeHandler(IWriteRepository<OfficeEntity> writeRepository, IReadRepository<OfficeEntity> readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }

    public async Task<ResponseModel<UpdateOfficeResponse>> Handle(UpdateOfficeCommand command, CancellationToken cancellationToken)
    {
        var office = await _readRepository.GetByAsync(p => p.Id == command.Id && !p.IsDeprecated);
        if (office is null)
        {
            throw new NotFoundException("Office not found.");
        }

        office = UpdateOffice(office, command);
        _writeRepository.Update(office);

        var status = await _writeRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }

        return new ResponseModel<UpdateOfficeResponse>
        {
            Message = "Office updated successfully.",
            Data = LockMapper.Mapper.Map<UpdateOfficeResponse>(office)
        };
    }

    private OfficeEntity UpdateOffice(OfficeEntity entity, UpdateOfficeCommand command)
    {
        entity.Address = command.Address;
        entity.Country = command.Country;
        entity.Description = command.Description;
        entity.Name = command.Name;
        entity.State = command.Name;
        entity.NumberOfDoors = command.NumberOfDoors;
        entity.NumberOfLocks = command.NumberOfLocks;
        entity.UpdatedAt = DateTime.UtcNow;
        return entity;
    }
}