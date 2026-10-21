using DotnetManager.Cli;
using DotnetManager.Cli.Abstractions;
using DotnetManager.Cli.Commands.Available;
using DotnetManager.Cli.Commands.Available.Abstraction;
using DotnetManager.Cli.Commands.Available.Services;
using DotnetManager.Cli.Commands.List;
using DotnetManager.Cli.Commands.Update;
using DotnetManager.InstalledDotnet.DependencyInjection;
using DotnetManager.ReleaseMetadata.DependencyInjection;
using DotnetManager.UserEnvironment.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetManager.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDotnetManager(this IServiceCollection services)
    {
        return services
            .AddReleaseMetadata()
            .AddUserEnvironment()
            .AddInstalledDotnet()
            .AddCli();
    }

    private static IServiceCollection AddCli(this IServiceCollection services)
    {
        services.AddSingleton<ISdkReleaseService, SdkReleaseService>();
        services.AddTransient<ICommand, AvailableCommand>();
        services.AddTransient<ICommand, ListCommand>();
        services.AddTransient<ICommand, UpdateCommand>();
        services.AddSingleton<CommandRegistration>();

        return services;
    }

}
