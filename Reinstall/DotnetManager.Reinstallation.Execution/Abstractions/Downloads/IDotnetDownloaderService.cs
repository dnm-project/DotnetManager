using DotnetManager.Reinstallation.Models.Downloads;

namespace DotnetManager.Reinstallation.Abstractions.Downloads;

internal interface IDotnetDownloaderService
{
    Task<DotnetDownload> DownloadAsync(DotnetDownloadSource downloadSource, CancellationToken cancellationToken);
}