namespace DotnetManager.Installation.Planning.Models;

public sealed record InstallPlanningRequest(IReadOnlyList<PlanningComponent> Components, string TargetRoot);