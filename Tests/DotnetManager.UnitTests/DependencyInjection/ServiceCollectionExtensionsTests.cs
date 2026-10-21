using DotnetManager.DependencyInjection;
using DotnetManager.UserEnvironment.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetManager.UnitTests.DependencyInjection;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddDotnetManagerRegistersResolvableApplicationServices()
    {
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDotnetManager();

        using var provider = services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateOnBuild = true,
            ValidateScopes = true
        });

        Assert.NotNull(provider.GetRequiredService<IUEManager>());
    }
}
