namespace PolarisEcl.Application.Common.Interfaces;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(Stream fileStream, string fileName, string folderName);
    Task DeleteFileAsync(string storagePath);
    Task<Stream> GetFileAsync(string fileName);
}