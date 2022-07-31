using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;

namespace LockManagementSystem.Application.Interface.Auth;

public interface ITokenService
{
    public Task<AuthResponse> GenerateToken(EmployeeEntity user );
}