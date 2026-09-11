using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Enums;
using PolarisEcl.Domain.Exceptions;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Application.Services;

public class TemplateService : ITemplateService
{
    private readonly IAppDbContext _context;
    private readonly IFileStorageService _storageService;
    private readonly ILogger<TemplateService> _logger;

    public TemplateService(IAppDbContext context, IFileStorageService storageService, ILogger<TemplateService> logger)
    {
        _context = context;
        _storageService = storageService;
        _logger = logger;
    }

    public async Task<string> UploadSystemTemplateAsync(UploadTemplateRequestDto request, Guid userId)
    {

        var existingTemplate = await _context.ComputationFiles.FirstOrDefaultAsync(f => f.File == request.FileType && f.ComputationId == null);


        if (existingTemplate != null)
        {
            _logger.LogInformation("Replacing existing system template for {FileType}", request.FileType);

            await _storageService.DeleteFileAsync(existingTemplate.StoragePath);

            using var stream = request.File.OpenReadStream();
            string newStoragePath = await _storageService.SaveFileAsync(stream, request.File.FileName, "Templates");

            existingTemplate.FileName = request.File.FileName;
            existingTemplate.StoragePath = newStoragePath;
            existingTemplate.UploadedAt = DateTime.UtcNow;
            existingTemplate.UploadedById = userId;

            _context.ComputationFiles.Update(existingTemplate);
        }
        else
        {
            using var stream = request.File.OpenReadStream();
            string relativeStoragePath = await _storageService.SaveFileAsync(stream, request.File.FileName, "Templates");

            var newTemplate = new ComputationFile
            {
                Id = Guid.NewGuid(),
                ComputationId = null,
                File = request.FileType,
                FileName = request.File.FileName,
                StoragePath = relativeStoragePath,
                UploadedAt = DateTime.UtcNow,
                UploadedById = userId
            };

            _context.ComputationFiles.Add(newTemplate);
        }

        await _context.SaveChangesAsync();
        return "Upload template updated successfully.";
    }

    public async Task<string> UploadUserComputationFileAsync(UploadTemplateRequestDto request, Guid userId)
    {
        var computationExists = await _context.ECLComputations.AnyAsync(f => f.Id == request.ComputationId);

        if (!computationExists)
        {
            throw new NotFoundException("The specified computation does not exist.");
        }

        using var stream = request.File.OpenReadStream();
        string relativeStoragePath = await _storageService.SaveFileAsync(stream, request.File.FileName, "Uploads");

        var userFile = new ComputationFile
        {
            Id = Guid.NewGuid(),
            ComputationId = request.ComputationId,
            File = request.FileType,
            FileName = request.File.FileName,
            StoragePath = relativeStoragePath,
            UploadedAt = DateTime.UtcNow,
            UploadedById = userId
        };

        _context.ComputationFiles.Add(userFile);
        await _context.SaveChangesAsync();

        return "File uploaded successfully.";

    }
    public async Task<DownloadTemplateDto> DownloadDefaultTemplateAsync(FileType fileType)
    {
        _logger.LogInformation($"Processing template download for {fileType}");

        var templateFile = await _context.ComputationFiles
            .Where(f => f.ComputationId == null)
            .Where(f => f.File == fileType)
            .OrderByDescending(f => f.UploadedAt)
            .FirstOrDefaultAsync();

        if (templateFile is null)
        {
            _logger.LogWarning($"System template requested was not found in database for type: {fileType}");
            throw new NotFoundException($"The requested template framework for type '{fileType}' has not been configured in the system yet.");
        }

        var fileStream = await _storageService.GetFileAsync(templateFile.StoragePath);
        var extension = Path.GetExtension(templateFile.FileName).ToLowerInvariant();
        var contentType = extension switch
        {
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".csv" => "text/csv",
            _ => "application/octet-stream"
        };

        _logger.LogInformation("Successful");
        return new DownloadTemplateDto(fileStream, contentType, templateFile.FileName);
    }

    public async Task<IEnumerable<TemplateMetadataDto>> GetDefaultTemplatesAsync()
    {
        var allTemplates = await _context.ComputationFiles.Where(f => f.ComputationId == null).Select(d => new TemplateMetadataDto(
            Id: d.Id,
            FileType: d.File,
            FileName: d.FileName,
            UploadedAt: d.UploadedAt
        )).ToListAsync();

        return allTemplates;
    }
    public async Task<string> DeleteDefaultTemplateAsync(Guid templateId)
    {
        var templateFile = await _context.ComputationFiles.SingleOrDefaultAsync(f => f.ComputationId == null && f.Id == templateId);
        if (templateFile == null)
        {
            _logger.LogWarning($"File not found for the default template Id {templateId}");
            throw new NotFoundException("Default template not found.");
        }

        await _storageService.DeleteFileAsync(templateFile.StoragePath);
        _context.ComputationFiles.Remove(templateFile);

        return "Successfully Deleted Default Template";
    }
}