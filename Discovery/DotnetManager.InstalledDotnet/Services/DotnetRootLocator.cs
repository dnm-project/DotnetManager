using DotnetManager.InstalledDotnet.Abstractions;
using Microsoft.Extensions.Logging;

namespace DotnetManager.InstalledDotnet.Services;

internal sealed class DotnetRootLocator(
    IEnumerable<IDotnetRootSource> sources,
    ILogger<DotnetRootLocator> logger) : IDotnetRootLocatorService
{
    public IReadOnlyCollection<string> GetRoots()
    {
        var roots = FindRoots()
            .Distinct(StringComparer.Ordinal)
            .ToList();

        logger.LogDebug("Discovered {RootCount} .NET root(s): {Roots}",
            roots.Count, string.Join(", ", roots));
        return roots;
    }

    private IEnumerable<string> FindRoots()
    {
        foreach (var source in sources)
        {
            foreach (var root in source.DiscoverRoots())
            {
                if (!Directory.Exists(root))
                    continue;

                yield return Path.GetFullPath(root);
            }
        }
    }
}
