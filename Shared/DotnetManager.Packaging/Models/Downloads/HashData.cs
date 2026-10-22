namespace DotnetManager.Packaging.Models.Downloads;

internal sealed record HashData(bool Verified, string ExpectedHash, string ActualHash);