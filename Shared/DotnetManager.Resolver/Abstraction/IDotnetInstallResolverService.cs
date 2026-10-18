using DotnetManager.Resolver.Models.Requests;
using DotnetManager.Resolver.Models.Results;

namespace DotnetManager.Resolver.Abstraction;

internal interface IDotnetInstallResolverService
{
    Task<ResolvedInstallation> ResolveAsync(InstallRequest request, CancellationToken cancellationToken);
}