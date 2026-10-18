using Microsoft.Extensions.DependencyInjection;

namespace DotnetManager.Installation.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInstallation(this IServiceCollection services)
    {

        return services;
    }
}