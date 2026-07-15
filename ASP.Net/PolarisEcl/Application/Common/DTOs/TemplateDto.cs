using Microsoft.AspNetCore.Http;
using PolarisEcl.Domain.Enums;
using PolarisEcl.Domain.Models;

namespace PolarisEcl.Application.Common.Interfaces;

public class UploadTemplateRequestDto
{
   public IFormFile File { get; set; } = null!;
   public FileType FileType { get; set; }
   public Guid? ComputationId { get; set; }
}

// response Dto
public record TemplateMetadataListDto(
    IEnumerable<TemplateMetadataDto> Templates
);

public record TemplateMetadataDto
(
   Guid Id,
   FileType FileType,
   string FileName,
   DateTime UploadedAt
);

public record DownloadTemplateDto
(
   Stream FileStream,
   string ContentType,
   string FileName
);
