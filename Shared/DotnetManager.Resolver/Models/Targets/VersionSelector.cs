using NuGet.Versioning;

namespace DotnetManager.Resolver.Models.Targets;

public sealed record VersionSelector(NuGetVersion Version, int Precision);