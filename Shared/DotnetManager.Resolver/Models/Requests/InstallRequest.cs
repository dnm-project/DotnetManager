using DotnetManager.Core.Models;
using DotnetManager.Resolver.Models.Targets;

namespace DotnetManager.Resolver.Models.Requests;

public sealed record InstallRequest(InstallTarget Target, IReadOnlyCollection<DotnetComponent> Components, string? Rid = null);