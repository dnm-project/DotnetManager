namespace DotnetManager.Packaging.Models.Downloads;

public sealed record DotnetDownload
{
    public required string FilePath { get; init; }
    public required string FileName { get; init; }
    public required string Hash { get; init; }
    public required string Rid { get; init; }

}