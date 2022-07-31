using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LockManagementSystem.Application.Interface.Auth;
using LockManagementSystem.Application.Models.Responses;
using LockManagementSystem.Domain.Entities;
using LockManagementSystem.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LockManagementSystem.Infrastructure.Services.Auth;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly LockManagementReadContext _dbContext;
    
    public TokenService(IConfiguration configuration, LockManagementReadContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }
    
    public async Task<AuthResponse> GenerateToken(EmployeeEntity employee)
    {
        var presentDateTime = DateTime.UtcNow;
        var validTimeInSeconds = int.Parse(_configuration["TokenTTL"]);
        var expiresIn = presentDateTime.AddSeconds(validTimeInSeconds);
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

        // Retrieve user roles
        var roles = await GetRoles(employee.Id);

        var employeeRoles = roles.Select(role => new Claim(ClaimTypes.Role, role.RoleId.ToString())).ToArray();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new ("userid", employee.Id.ToString()),
                new (ClaimTypes.Email, employee.Email)
            }.Union(employeeRoles)),
            Expires = expiresIn,
            IssuedAt = presentDateTime,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new AuthResponse
        {
            Token = tokenHandler.WriteToken(token),
            CreatedAt = presentDateTime,
            ValidFor = validTimeInSeconds
        };
    }

    private async Task<List<EmployeeRoleEntity>> GetRoles(Guid employeeId)
    {
        return await _dbContext.EmployeeRoles.Where(p => p.EmployeeId == employeeId && !p.IsDeprecated)
            .ToListAsync();
    }
}