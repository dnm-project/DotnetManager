namespace DotnetManager.Resolver.Models.Results;

public sealed record ResolvedInstallation(IReadOnlyList<ResolvedComponent> Components);