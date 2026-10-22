using DotnetManager.Packaging.Models.Downloads;

namespace DotnetManager.Packaging.Abstractions.Archives;

internal interface IArchiveExtractorService
{
    Task ExtractAsync(DotnetDownload download, string destinationPath, CancellationToken cancellationToken);
}