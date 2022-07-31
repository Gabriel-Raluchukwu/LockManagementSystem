using System.Reflection;
using System.Text;
using LockManagementSystem.Application.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace LockManagementSystem.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterMediatR(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
    
    public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
    
    public static IServiceCollection ConfigureJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettings = configuration["Jwt:Key"];
        var key = Encoding.ASCII.GetBytes(appSettings);
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(p =>
            {
                p.SaveToken = true;
                p.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

        return services;
    }
    
    public static void ConfigureExceptionMiddleware(this WebApplication app) 
    { 
        app.UseMiddleware<ExceptionMiddleware>(); 
    }
}
