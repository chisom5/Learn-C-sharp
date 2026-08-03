using FluentValidation;
using PolarisEcl.Application.Common.Dtos;

namespace PolarisEcl.Application.Common.Validators;

public class AddNewComputationRequestDtoValidator : AbstractValidator<AddNewComputationRequestDto>
{
    public AddNewComputationRequestDtoValidator()
    {
        RuleFor(c => c.Month).NotEmpty().WithMessage("Month is required");

        RuleFor(x => x.Year)
       .Cascade(CascadeMode.Stop)
       .NotEmpty().WithMessage("Year is required")
       .ValidFinancialYear(9);
    }
}

public class ModelInfoRequestDtoValidator : AbstractValidator<ModelInfoRequestDto>
{
    public ModelInfoRequestDtoValidator()
    {
        RuleFor(x => x.ComputationId)
            .NotEmpty()
            .WithMessage("Computation ID is required.");

        RuleFor(x => x.Pd_Baseline)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Baseline PD weight cannot be negative.");

        RuleFor(x => x.Pd_Bestcase)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Bestcase PD weight cannot be negative.");

        RuleFor(x => x.Pd_Worstcase)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Worstcase PD weight cannot be negative.");

        RuleFor(x => x)
            .Must(x => x.Pd_Baseline + x.Pd_Bestcase + x.Pd_Worstcase == 100)
            .WithMessage("The sum of Baseline, Bestcase, and Worstcase PD weights must equal 100%.")
            .WithName("PdWeights"); // Optionally group this error under a specific key name

        RuleFor(x => x.AdjustmentFactor).IsInEnum().WithMessage("Invalid Macroeconomic Adjustment Factor entered.");
        RuleFor(x => x.HistoricalMargin).IsInEnum().WithMessage("Invalid Historical margin entered.");
    }
}