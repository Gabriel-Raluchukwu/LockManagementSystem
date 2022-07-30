using LockManagementSystem.Application.Interface;
using LockManagementSystem.Application.Interface.EventLog;
using LockManagementSystem.Infrastructure.Services;
using LockManagementSystem.Infrastructure.Services.Repositories.EventLog;
using Microsoft.Extensions.DependencyInjection;

namespace LockManagementSystem.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        
        //custom services
        services.AddTransient<IEventLogReadRepository, EventLogReadRepository>();
        return services;
    }
}