using DotnetManager.Core.Models;
using DotnetManager.Resolver.Models.Results;

namespace DotnetManager.Resolver.Abstraction;

internal interface IInstallResolver<in TSelector>
{
    Task<ResolvedInstallation> ResolveAsync(
        TSelector selector,
        IEnumerable<DotnetComponent> components,
        string? rid,
        CancellationToken cancellationToken);
}
