using FluentValidation;
using PolarisEcl.Application.Common.Dtos;

namespace PolarisEcl.Application.Common.Validators;

public class DashboardRequestDtoValidator : AbstractValidator<DashboardRequestDto>
{
    public DashboardRequestDtoValidator()
    {
        RuleFor(x => x.Month)
        .InclusiveBetween(1, 12)
        .WithMessage("Month must be between 1 (January) and 12 (December).");

        RuleFor(x => x.Year)
        .NotEmpty()
        .ValidFinancialYear(10)
        .WithMessage("Year is required to load dashboard analytics.");
    }
}