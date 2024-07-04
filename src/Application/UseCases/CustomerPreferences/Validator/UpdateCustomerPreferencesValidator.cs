using Application.DTOs.CustomerPreferences;
using FluentValidation;

namespace Application.UseCases.CustomerPreferences.Validator;

public class UpdateCustomerPreferencesValidator : AbstractValidator<UpdateCustomerPreferencesRequest>
{
    public UpdateCustomerPreferencesValidator()
    {
        RuleForEach(x => x.PreferredPurchaseDays)
            .NotEmpty().WithMessage("PreferredPurchaseDays is required.")
            .Must(BeAValidDayOfWeek).WithMessage("PreferredPurchaseDays must be a valid day of the week.");
    }

    private bool BeAValidDayOfWeek(string day)
    {
        return Enum.TryParse<DayOfWeek>(day, true, out _);
    }
}