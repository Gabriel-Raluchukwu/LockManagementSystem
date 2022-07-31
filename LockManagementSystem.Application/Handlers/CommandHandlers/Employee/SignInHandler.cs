using LockManagementSystem.Application.Exceptions;
using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Interface.Auth;
using LockManagementSystem.Application.Mappings;
using LockManagementSystem.Application.Models.Commands.Employee;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Handlers.CommandHandlers.Employee;

public class SignInHandler : IRequestHandler<SignInCommand, ResponseModel<SignInResponse>>
{
    private readonly IReadRepository<EmployeeEntity> _employeeReadRepository;
    private readonly IPasswordService _passwordService;
    private readonly ITokenService _tokenService;
    
    public SignInHandler(IReadRepository<EmployeeEntity> employeeReadRepository, IPasswordService passwordService,
        ITokenService tokenService)
    {
        _employeeReadRepository = employeeReadRepository;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    public async Task<ResponseModel<SignInResponse>> Handle(SignInCommand command, CancellationToken cancellationToken)
    {
        var employee = await _employeeReadRepository.GetByAsync(p => p.Email.ToLower() == command.Email && !p.IsDeprecated);
        if (employee is null)
        {
            throw new NotFoundException("Employee not found. Please contact Clay helpdesk.");
        }

        var isPasswordValid = _passwordService.Verify(command.Password, employee.PasswordHash);
        
        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Email or password is not valid.");
        }
        
        var token = await _tokenService.GenerateToken(employee);
        
        return new ResponseModel<SignInResponse>
        {
            Message = "Employee sign in successful.",
            Data = LockMapper.Mapper.Map<SignInResponse>(token)
        };
    }
}