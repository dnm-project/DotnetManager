using DotnetManager.Installation.Resolver.Models.Results;

namespace DotnetManager.Installation.Planning.Models;

public sealed record PlanningComponent(ResolvedComponent Component, ReinstallRule ReinstallRule);