using Microsoft.Extensions.DependencyInjection;

namespace DatingApp.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

        services.AddAutoMapper(currentAssemblies);

        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblies(currentAssemblies));

        return services;
    }
}