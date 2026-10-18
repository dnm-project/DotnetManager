using DotnetManager.Installation.Planning.Abstraction;
using DotnetManager.Installation.Planning.Models;
using DotnetManager.Installation.Resolver.Models.Results;
using DotnetManager.InstalledDotnet.Abstractions;
using DotnetManager.InstalledDotnet.Models;

namespace DotnetManager.Installation.Planning.Services;

public class InstallationPlanner : IInstallationPlanner
{
    private readonly IDotnetInstallationLocatorService<SdkInstallation> _sdkService;
    private readonly IDotnetInstallationLocatorService<RuntimeInstallation> _runtimeService;

    public InstallationPlanner(IDotnetInstallationLocatorService<SdkInstallation> sdkService,
        IDotnetInstallationLocatorService<RuntimeInstallation> runtimeService)
    {
        _sdkService = sdkService;
        _runtimeService = runtimeService;
    }

    public Task<IReadOnlyList<ResolvedComponent>> PlanAsync(InstallPlanningRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}