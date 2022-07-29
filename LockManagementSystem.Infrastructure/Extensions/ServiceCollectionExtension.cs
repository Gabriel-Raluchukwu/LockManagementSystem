using LockManagementSystem.Application.Interface;
using LockManagementSystem.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LockManagementSystem.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        return services;
    }
}