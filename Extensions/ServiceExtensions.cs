using TaskManager.Api.Services;

namespace TaskManager.Api.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<TaskService>();
        services.AddScoped<AuthService>();
        return services;
    }
}