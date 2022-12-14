using LockManagementSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace LockManagementSystem.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        services.AddRouting(options => options.LowercaseUrls = true);
        services.AddSwaggerGen(swagger =>
        {
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Description = @"JWT Authorization header using the Bearer scheme.
                                  <br/>Enter your token in the text input below.
                                  <br/>Example: '12345abcdef'",
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Type = SecuritySchemeType.Http
            });
            
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });
        
        return services;
    }
    
    public static void MigrateDatabase(this WebApplication app)
    {
        var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<LockManagementWriteContext>();
        context.Database.Migrate();
    }
}