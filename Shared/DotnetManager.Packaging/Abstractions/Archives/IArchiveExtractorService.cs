using DotnetManager.Packaging.Models.Downloads;

namespace DotnetManager.Packaging.Abstractions.Archives;

internal interface IArchiveExtractorService
{
    Task ExtractAsync(string filePath, string fileName, string destinationPath, CancellationToken ct);
}