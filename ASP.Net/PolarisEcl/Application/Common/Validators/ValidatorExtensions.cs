
using FluentValidation;

namespace PolarisEcl.Application.Common.Validators;

public static class ValidatorExtensions
{
    public static IRuleBuilderOptions<T, int> ValidFinancialYear<T>(this IRuleBuilder<T, int> ruleBuilder, int yearsBack)
    {
        return ruleBuilder.Must(yearValue =>
        {
            int currentYear = DateTime.UtcNow.Year;
            int startYear = currentYear - yearsBack;

            return yearValue >= startYear && yearValue <= currentYear;
        }).WithMessage($"Please Enter a financial year between {DateTime.UtcNow.Year - yearsBack} and {DateTime.UtcNow.Year}");
    }
}