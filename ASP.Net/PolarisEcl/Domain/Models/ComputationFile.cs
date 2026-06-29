using PolarisEcl.Domain.Enums;

namespace PolarisEcl.Domain.Models;

public class ComputationFile
{
    public Guid Id { get; set; }
    public Guid ComputationId { get; set; } 
    public ECLComputation ECLComputation { get; set; } = null!;
    public FileType File { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string StoragePath { get; set; } = string.Empty;
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    public Guid UploadedById { get; set; } //userId of who uploaded the file.
    public User UploadedBy { get; set; } = null!;
}