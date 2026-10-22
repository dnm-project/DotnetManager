using System.Diagnostics;
using System.Security.Cryptography;
using DotnetManager.Packaging.Abstractions.Downloads;
using DotnetManager.Packaging.Exceptions;
using DotnetManager.Packaging.Models.Downloads;
using DotnetManager.ReleaseMetadata.Models.Releases;
using Microsoft.Extensions.Logging;

namespace DotnetManager.Packaging.Services.Downloads;

internal sealed class DotnetDownloader(HttpClient client, ILogger<DotnetDownloader> logger) : IDotnetDownloaderService
{
    public async Task<DotnetDownload> DownloadAsync(ReleaseFile file, CancellationToken cancellationToken)
    {
        var stopwatch = Stopwatch.StartNew();
        logger.LogInformation("Downloading {FileName} for {RuntimeIdentifier} from {DownloadUri}",
            file.FileName, file.Rid, file.Url);

        var tempPath = Path.GetTempPath();
        var fileName = $"{Guid.NewGuid():N}_{file.FileName}";
        var filePath = Path.Combine(tempPath, fileName);

        logger.LogDebug("Temporary download path for {FileName}: {FilePath}", file.FileName, filePath);

        try
        {
            var downloadFile = client.GetStreamAsync(file.Url, cancellationToken);


            await using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await using var downloadStream = await downloadFile;
                await downloadStream.CopyToAsync(fileStream, cancellationToken);
            }


            logger.LogInformation("Downloaded {FileName} to {FilePath} in {ElapsedSeconds:F1} seconds",
                file.FileName, filePath, stopwatch.Elapsed.TotalSeconds);
            logger.LogInformation("Verifying SHA-512 checksum for {FileName}", file.FileName);
            var hash = await VerifyHash(filePath, file.Hash, cancellationToken);

            if (!hash.Verified)
            {
                logger.LogWarning(
                    "SHA-512 verification failed for {FileName}; expected {ExpectedHash}, received {ActualHash}; deleting {FilePath}",
                    file.FileName, hash.ExpectedHash, hash.ActualHash, filePath);
                File.Delete(filePath);
                throw new DownloadHashMismatchException(filePath, hash.ExpectedHash, hash.ActualHash);
            }

            logger.LogInformation("SHA-512 verification succeeded for {FileName}", file.FileName);
            logger.LogDebug("Verified SHA-512 checksum {ActualHash} for {FileName}",
                hash.ActualHash, file.FileName);

            return new DotnetDownload
            {
                FilePath = filePath,
                FileName = fileName,
                Hash = file.Hash,
                Rid = file.Rid
            };
        }
        catch
        {
            File.Delete(filePath);
            throw;
        }


    }

    private static async Task<HashData> VerifyHash(string filePath,
        string expectedHash,
        CancellationToken cancellationToken)
    {
        await using var fileStream = new FileStream(filePath, FileMode.Open);
        var hash = await SHA512.HashDataAsync(fileStream, cancellationToken);
        var hashString = Convert.ToHexStringLower(hash);
        var verified = string.Equals(hashString, expectedHash, StringComparison.OrdinalIgnoreCase);
        return new HashData(verified, expectedHash, hashString);
    }
}