using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LockManagementSystem.Infrastructure.Persistence;

public static class PersistenceInfrastructure
{
    private static string DatabaseName = "Database";
    
    public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LockManagementReadContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(DatabaseName)));
            //options.UseInMemoryDatabase(DatabaseName));
        
        services.AddDbContext<LockManagementWriteContext>(options => 
            options.UseNpgsql(configuration.GetConnectionString(DatabaseName)));
            //options.UseInMemoryDatabase(DatabaseName));

        return services;
    }
}