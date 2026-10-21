using DotnetManager.InstalledDotnet.Abstractions;
using DotnetManager.InstalledDotnet.Models;
using NuGet.Versioning;

namespace DotnetManager.InstalledDotnet.Services;

internal sealed class RuntimeLocator(IDotnetRootLocatorService rootLocator)
    : IDotnetInstallationLocatorService<RuntimeInstallation>
{
    public IReadOnlyCollection<RuntimeInstallation> Find()
    {
        return rootLocator.GetRoots()
            .SelectMany(FindRuntimes)
            .ToList();
    }

    private static IEnumerable<RuntimeInstallation> FindRuntimes(string? root)
    {
        if (root is null)
            yield break;

        var sharedRoot = Path.Combine(root, "shared");

        if (!Directory.Exists(sharedRoot))
            yield break;

        foreach (var frameworkPath in Directory.EnumerateDirectories(sharedRoot))
        {
            var framework = Path.GetFileName(frameworkPath);

            foreach (var versionPath in Directory.EnumerateDirectories(frameworkPath))
            {
                if (!NuGetVersion.TryParse(Path.GetFileName(versionPath), out var version))
                    continue;

                yield return new RuntimeInstallation(framework, version, versionPath);
            }
        }
    }
}
