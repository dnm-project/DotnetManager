using DotnetManager.Installation.Planning.Models;
using DotnetManager.Resolver.Models.Results;

namespace DotnetManager.Installation.Planning.Abstraction;

public interface IInstallationPlanner
{
    Task<IReadOnlyList<ResolvedComponent>> PlanAsync(InstallPlanningRequest request,
        CancellationToken cancellationToken);
}