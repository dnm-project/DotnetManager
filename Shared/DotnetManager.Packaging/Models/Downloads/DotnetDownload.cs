namespace DotnetManager.Packaging.Models.Downloads;

public sealed record DotnetDownload(string FilePath, string FileName, string Hash, string Rid);