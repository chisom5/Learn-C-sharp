using FluentValidation;
using PolarisEcl.Application.Common.Dtos;
using PolarisEcl.Application.Common.Interfaces;
using PolarisEcl.Domain.Enums;

namespace PolarisEcl.Application.Common.Validators;

public class TemplateDtoValidator : AbstractValidator<UploadTemplateRequestDto>
{
    public TemplateDtoValidator()
    {
        RuleFor(x => x.File)
            .NotNull().WithMessage("File is required.")
            .Must(file => file.Length > 0).WithMessage("File cannot be empty.")
            .Must(x => Path.GetExtension(x.FileName).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)
                       || Path.GetExtension(x.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Only Excel (.xlsx) or CSV (.csv) template formats are allowed.");

        RuleFor(x => x.FileType)
            .IsInEnum().WithMessage("Invalid file type.");

        When(x => x.ComputationId != null, () =>
        {
            RuleFor(x => x.ComputationId)
           .NotEmpty().WithMessage("A specific Computation ID is required for user file runs.");
        });
    }

}