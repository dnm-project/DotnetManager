using DotnetManager.Packaging.Models.Downloads;
using DotnetManager.ReleaseMetadata.Models.Releases;

namespace DotnetManager.Packaging.Abstractions.Downloads;

internal interface IDotnetDownloaderService
{
    Task<DotnetDownload> DownloadAsync(ReleaseFile file, CancellationToken cancellationToken);
}