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
    private readonly IReadRepository<OfficeEntity> _officeReadRepository;
    private readonly IWriteRepository<EmployeeDetailEntity> _employeeDetailWriteRepository;
    private readonly IReadRepository<EmployeeDetailEntity> _employeeDetailReadRepository;

    public RegisterEmployeeHandler(IWriteRepository<EmployeeDetailEntity> employeeDetailWriteRepository, IReadRepository<EmployeeDetailEntity> employeeDetailReadRepository,
        IReadRepository<OfficeEntity> officeReadRepository)
    {
        _officeReadRepository = officeReadRepository;
        _employeeDetailWriteRepository = employeeDetailWriteRepository;
        _employeeDetailReadRepository = employeeDetailReadRepository;
    }
    
    public async Task<ResponseModel<EmployeeDetailsResponse>> Handle(RegisterEmployeeCommand command, CancellationToken cancellationToken)
    {
        var office = await _officeReadRepository.GetByAsync(p => p.Id == command.OfficeId && !p.IsDeprecated);
        if (office is null)
        {
            throw new NotFoundException("Office not found.");
        }
        
        if (string.IsNullOrWhiteSpace(command.Email))
        {
            var generatedEmail = $"{command.FirstName.ToLower()}.{command.LastName.ToLower()}@clay.com";
            command.Email = generatedEmail.ToLower();
        }

        var duplicate = await _employeeDetailReadRepository.GetByAsync(p => p.Email.ToLower() == command.Email && !p.IsDeprecated);
        if (duplicate is not null)
        {
            throw new BadRequestException($"Email {command.Email} already exists.");
        }

        var entity = LockMapper.Mapper.Map<EmployeeDetailEntity>(command);
        
        _employeeDetailWriteRepository.Insert(entity);

        var status = await _employeeDetailWriteRepository.SaveChangesAsync(cancellationToken) > 0;

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