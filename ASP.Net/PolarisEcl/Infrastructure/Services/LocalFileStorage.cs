using Microsoft.AspNetCore.Hosting;
using PolarisEcl.Application.Common.Interfaces;

namespace PolarisEcl.Infrastructure.Services;

public class LocalFileStorage : IFileStorageService
{
    private readonly string _contentRootPath;

    public LocalFileStorage(IWebHostEnvironment environment)
    {
        _contentRootPath = Path.Combine(environment.ContentRootPath, "FileStorage");
        if (!Directory.Exists(_contentRootPath))
        {
            Directory.CreateDirectory(_contentRootPath);
        }
    }

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string folderName)
    {
        var targetFolder = Path.Combine(_contentRootPath, folderName);
        if (!Directory.Exists(targetFolder))
        {
            Directory.CreateDirectory(targetFolder);
        }
        var safeFileName = Path.GetFileName(fileName);
        var uniqueFileName = $"{Guid.NewGuid()}_{safeFileName}";
        var absolutePath = Path.Combine(targetFolder, uniqueFileName);

        using (var fileStreamToWrite = new FileStream(absolutePath, FileMode.Create))
        {
            await fileStream.CopyToAsync(fileStreamToWrite);
        }

        return Path.Combine(folderName, uniqueFileName).Replace("\\", "/");
    }

    public Task DeleteFileAsync(string storagePath)
    {
        if (string.IsNullOrEmpty(storagePath)) return Task.CompletedTask;

        var absolutePath = Path.Combine(_contentRootPath, storagePath.Replace("/", Path.DirectorySeparatorChar.ToString()));

        if (File.Exists(absolutePath))
        {
            File.Delete(absolutePath);
        }

        return Task.CompletedTask;
    }

    public Task<Stream> GetFileAsync(string fileName)
    {
        var filePath = Path.Combine(_contentRootPath, fileName.Replace("/", Path.DirectorySeparatorChar.ToString()));
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file '{fileName}' was not found.");
        }

        Stream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return Task.FromResult(fileStream);

        // var memoryStream = new MemoryStream();
        // using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        // {
        //     await fileStream.CopyToAsync(memoryStream);
        // }
        // memoryStream.Position = 0; // Reset the stream position to the beginning
        // return memoryStream;
    }
}