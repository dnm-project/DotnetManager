using System.Diagnostics;
using System.Formats.Tar;
using System.IO.Compression;
using DotnetManager.Packaging.Abstractions.Archives;
using DotnetManager.Packaging.Models.Downloads;
using Microsoft.Extensions.Logging;

namespace DotnetManager.Packaging.Services.Archives;

internal sealed class ArchiveExtractor(ILogger<ArchiveExtractor> logger) : IArchiveExtractorService
{
    public async Task ExtractAsync(string filePath, string fileName, string destinationPath, CancellationToken ct)
    {
        var stopwatch = Stopwatch.StartNew();
        Directory.CreateDirectory(destinationPath);

        var tempName = fileName.Split('_')[1];
        var newFileName = tempName.Split('-')[1];

        var archivePath = Path.Combine(destinationPath, newFileName);

        logger.LogInformation("Extracting {ArchivePath} to {DestinationPath}",
            archivePath, destinationPath);

        if (archivePath.EndsWith(".tar.gz", StringComparison.OrdinalIgnoreCase))
        {
            await ExtractTarGzAsync(archivePath, destinationPath, ct);
            LogCompleted(archivePath, stopwatch.Elapsed);
            return;
        }

        logger.LogWarning("Archive format is not supported for {ArchivePath}", archivePath);
        throw new NotSupportedException($"Archive format is not supported: {archivePath}");
    }

    private static async Task ExtractTarGzAsync(string archivePath,
        string destinationPath,
        CancellationToken cancellationToken)
    {
        await using var fileStream = File.OpenRead(archivePath);
        await using var gzipStream = new GZipStream(fileStream, CompressionMode.Decompress);

        await TarFile.ExtractToDirectoryAsync(gzipStream, destinationPath,
            true, cancellationToken);
    }

    private void LogCompleted(string archivePath, TimeSpan elapsed)
    {
        logger.LogInformation("Extracted {ArchivePath} in {ElapsedSeconds:F1} seconds",
            archivePath, elapsed.TotalSeconds);
    }
}