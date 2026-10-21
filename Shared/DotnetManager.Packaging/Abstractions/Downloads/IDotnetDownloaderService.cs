using DotnetManager.Packaging.Models.Downloads;

namespace DotnetManager.Packaging.Abstractions.Downloads;

internal interface IDotnetDownloaderService
{
    Task<DotnetDownload> DownloadAsync(string downloadUrl, CancellationToken cancellationToken);
}