using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.Employee;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Application.Utility;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.Employee;

public class RegisterEmployeeHandler : IRequestHandler<RegisterEmployeeCommand, ResponseModel<EmployeeDetailsResponse>>
{
    private readonly IWriteRepository<EmployeeDetailEntity> _writeRepository;
    private readonly IReadRepository<EmployeeDetailEntity> _readRepository;

    public RegisterEmployeeHandler(IWriteRepository<EmployeeDetailEntity> writeRepository, IReadRepository<EmployeeDetailEntity> readRepository)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
    }
    
    public async Task<ResponseModel<EmployeeDetailsResponse>> Handle(RegisterEmployeeCommand command, CancellationToken cancellationToken)
    {
        //TODO: Add office validation
        
        if (string.IsNullOrWhiteSpace(command.Email))
        {
            var generatedEmail = $"{command.FirstName}.{command.LastName}@clay.com";
            command.Email = generatedEmail.ToLower();
        }

        var duplicate = await _readRepository.GetByAsync(p => p.Email.ToLower() == command.Email);
        if (duplicate is not null)
        {
            throw new BadRequestException($"Email {command.Email} already exists.");
        }

        var entity = LockMapper.Mapper.Map<EmployeeDetailEntity>(command);
        
        _writeRepository.Insert(entity);

        var status = await _writeRepository.SaveChangesAsync(cancellationToken) > 0;

        if (!status)
        {
            throw new ServerException(Constants.GenericErrorMessage);
        }

        return new ResponseModel<EmployeeDetailsResponse>
        {
            Message = "EmployeeEntity Detail saved successfully.",
            Data = LockMapper.Mapper.Map<EmployeeDetailsResponse>(entity)
        };
    }
}