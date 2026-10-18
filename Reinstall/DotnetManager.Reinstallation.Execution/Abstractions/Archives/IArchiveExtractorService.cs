namespace DotnetManager.Reinstallation.Abstractions.Archives;

internal interface IArchiveExtractorService
{
    Task ExtractAsync(string archivePath, string destinationPath, CancellationToken cancellationToken);
}