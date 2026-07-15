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

    public async Task<string> UploadTemplateAsync(UploadTemplateRequestDto request, Guid userId)
    {
        _logger.LogInformation($"Processing upload of template file. {request.File.FileName}");
        if (request.ComputationId == null)
        {
            var existingDefaultTemplate = await _context.ComputationFiles
                .FirstOrDefaultAsync(f => f.File == request.FileType && f.ComputationId == null);

            if (existingDefaultTemplate != null)
            {
                _logger.LogInformation("Found existing default template for {FileType}. Replacing file...", request.FileType);

                await _storageService.DeleteFileAsync(existingDefaultTemplate.StoragePath);

                _context.ComputationFiles.Remove(existingDefaultTemplate);
            }
        }

        string folderName = request.ComputationId == null ? "Templates" : "Uploads";

        using var stream = request.File.OpenReadStream();
        string relativeStoragePath = await _storageService.SaveFileAsync(stream, request.File.FileName, folderName);


        var computationFile = new ComputationFile
        {
            Id = Guid.NewGuid(),
            ComputationId = request.ComputationId,
            File = request.FileType,
            FileName = request.File.FileName,
            StoragePath = relativeStoragePath,
            UploadedAt = DateTime.UtcNow,
            UploadedById = userId

        };

        _context.ComputationFiles.Add(computationFile);
        await _context.SaveChangesAsync();

        _logger.LogInformation($"Successfully upload template of {request.File.FileName}");
        return "Upload Successful";
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