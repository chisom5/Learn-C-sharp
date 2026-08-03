using FluentValidation;
using PolarisEcl.Application.Common.Dtos;

namespace PolarisEcl.Application.Common.Validators;

public class SegmentRequestDtoValidator : AbstractValidator<SegmentRequestDto>
{
    public SegmentRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Segment name is required.");
    }
}

public class ProductRequestDtoValidator : AbstractValidator<ProductRequestDto>
{
    public ProductRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.");

        RuleFor(x => x.SegmentType)
            .NotEmpty().WithMessage("Segment type is required.");
    }
}

public class RegressionParamsRequestDtoValidator : AbstractValidator<RegressionParamsRequestDto>
{
    public RegressionParamsRequestDtoValidator()
    {
        RuleFor(x => x.RSquareLimit)
            .GreaterThan(0).WithMessage("RSquare limit must be greater than 0.");

        RuleFor(x => x.PValueSelected)
            .GreaterThan(0).WithMessage("P-Value selected must be greater than 0.");
    }
}

public class ExpectedCorrelationRequestDtoValidator : AbstractValidator<ExpectedCorrelationRequestDto>
{
    public ExpectedCorrelationRequestDtoValidator()
    {
        RuleFor(x => x.PrimeLendingRate)
            .IsInEnum().WithMessage("Invalid correlation direction for Prime Lending Rate.");

        RuleFor(x => x.Inflation)
            .IsInEnum().WithMessage("Invalid correlation direction for Inflation.");

        RuleFor(x => x.YieldOnTreasuryBills)
            .IsInEnum().WithMessage("Invalid correlation direction for Yield on Treasury Bills.");

        RuleFor(x => x.ExchangeRate)
            .IsInEnum().WithMessage("Invalid correlation direction for Exchange Rate.");

        RuleFor(x => x.MonetaryPolicyRate)
            .IsInEnum().WithMessage("Invalid correlation direction for Monetary Policy Rate.");
    }
}

public class CollateralTypeRequestDtoValidator : AbstractValidator<CollateralTypeRequestDto>
{
    public CollateralTypeRequestDtoValidator()
    {
        RuleFor(x => x.Type)
            .NotEmpty().WithMessage("Collateral type is required.");

        RuleFor(x => x.HaircutPercent)
            .GreaterThanOrEqualTo(0).WithMessage("Haircut percent must be greater than or equal to 0.");

        RuleFor(x => x.DurationYears)
            .GreaterThan(0).WithMessage("Duration in years must be greater than 0.");
    }
}

public class UpdateSegmentRequestDtoValidator : AbstractValidator<UpdateSegmentRequestDto>
{
    public UpdateSegmentRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Segment name cannot be empty.");

    }
}

public class UpdateProductRequestDtoValidator : AbstractValidator<UpdateProductRequestDto>
{
    public UpdateProductRequestDtoValidator()
    {

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name cannot be empty.");

        RuleFor(x => x.SegmentType)
            .NotEmpty().WithMessage("Segment type cannot be empty.");

    }
}

public class UpdateRegressionParamsRequestDtoValidator : AbstractValidator<UpdateRegressionParamsRequestDto>
{
    public UpdateRegressionParamsRequestDtoValidator()
    {
        RuleFor(x => x.RSquareLimit)
            .GreaterThan(0).WithMessage("RSquare limit must be greater than 0.");

        RuleFor(x => x.PValueSelected)
            .GreaterThan(0).WithMessage("P-Value selected must be greater than 0.");

    }
}