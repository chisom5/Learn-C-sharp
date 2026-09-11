using PolarisEcl.Domain.Enums;

namespace PolarisEcl.Application.Common.Interfaces;

public interface ITemplateService
{
    Task<string> UploadSystemTemplateAsync(UploadTemplateRequestDto request, Guid userId);
    Task<string> UploadUserComputationFileAsync(UploadTemplateRequestDto request, Guid userId);
    Task<string> DeleteDefaultTemplateAsync(Guid templateId);
    Task<DownloadTemplateDto> DownloadDefaultTemplateAsync(FileType fileType);
    Task<IEnumerable<TemplateMetadataDto>> GetDefaultTemplatesAsync();
}