using DotnetManager.Installation.Planning.Models;
using DotnetManager.Installation.Resolver.Models.Results;

namespace DotnetManager.Installation.Planning.Abstraction;

public interface IInstallationPlanner
{
    Task<IReadOnlyList<ResolvedComponent>> PlanAsync(InstallPlanningRequest request,
        CancellationToken cancellationToken);
}