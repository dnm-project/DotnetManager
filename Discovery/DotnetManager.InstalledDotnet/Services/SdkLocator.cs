using DotnetManager.InstalledDotnet.Abstractions;
using DotnetManager.InstalledDotnet.Models;
using NuGet.Versioning;

namespace DotnetManager.InstalledDotnet.Services;

internal sealed class SdkLocator(IDotnetRootLocatorService rootLocator)
    : IDotnetInstallationLocatorService<SdkInstallation>
{
    public IReadOnlyCollection<SdkInstallation> Find()
    {
        return rootLocator.GetRoots()
            .SelectMany(FindSdks)
            .ToList();
    }

    public IReadOnlyCollection<SdkInstallation> Find(string root)
    {
        return FindSdks(root).ToList();
    }

    private static IEnumerable<SdkInstallation> FindSdks(string? root)
    {
        if (root is null)
            yield break;

        var sdkRoot = Path.Combine(root, "sdk");

        if (!Directory.Exists(sdkRoot))
            yield break;

        foreach (var path in Directory.EnumerateDirectories(sdkRoot))
        {
            if (!NuGetVersion.TryParse(Path.GetFileName(path), out var version))
                continue;

            yield return new SdkInstallation(version, path);
        }
    }
}