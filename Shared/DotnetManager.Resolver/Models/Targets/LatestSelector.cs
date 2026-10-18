using DotnetManager.ReleaseMetadata.Models;

namespace DotnetManager.Resolver.Models.Targets;

public sealed record LatestSelector(ReleaseTypes? ReleaseType, SupportPhases? SupportPhase);