using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Interface.Auth;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.Employee;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.Employee;

public class SignUpHandler : IRequestHandler<SignUpCommand, ResponseModel<SignUpResponse>>
{
    private readonly IReadRepository<EmployeeDetailEntity> _employeeDetailReadRepository;
    private readonly IPasswordService _passwordService;
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    
    public SignUpHandler(IReadRepository<EmployeeDetailEntity> employeeDetailReadRepository, IPasswordService passwordService,
        IAuthService authService, ITokenService tokenService)
    {
        _employeeDetailReadRepository = employeeDetailReadRepository;
        _passwordService = passwordService;
        _authService = authService;
        _tokenService = tokenService;
    }

    public async Task<ResponseModel<SignUpResponse>> Handle(SignUpCommand command, CancellationToken cancellationToken)
    {
        var employeeDetail = await _employeeDetailReadRepository.GetByAsync(p => p.Id == command.EmployeeDetailId);
        if (employeeDetail is null)
        {
            throw new NotFoundException("Employee Detail not found. Please contact Clay helpdesk.");
        }

        var passwordHash = _passwordService.Hash(command.Password);
        var employee = new EmployeeEntity
        {
            Email = employeeDetail.Email,
            PasswordHash = passwordHash,
        };

        var employeeEntity = await _authService.SaveEmployeeWithRole(employee, null);

        if (employeeEntity == null)
        {
            throw new ServerException("Unable to process request.");
        }

        var token = await _tokenService.GenerateToken(employeeEntity);
        
        return new ResponseModel<SignUpResponse>
        {
            Message = "Employee sign up successful.",
            Data = LockMapper.Mapper.Map<SignUpResponse>(token)
        };
    }
}