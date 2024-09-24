using Application.DTOs.CustomerPreferences;
using FluentValidation;

namespace Application.UseCases.CustomerPreferences.Validator;

public class UpdateCustomerPreferencesValidator : AbstractValidator<UpdateCustomerPreferencesRequest>
{
    public UpdateCustomerPreferencesValidator()
    {
        RuleFor(x => x.PreferredPurchaseDays)
            .NotEmpty().WithMessage("PreferredPurchaseDay is required.")
            .NotNull().WithMessage("PreferredPurchaseDay is required.")
            .Must(BeAValidDayOfWeek).WithMessage("PreferredPurchaseDay must be a valid day of the week.");

        RuleFor(x => x.Quantity)
            .NotNull().WithMessage("Quantity is required.")
            .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
    }
    private bool BeAValidDayOfWeek(string day)
    {
        return Enum.TryParse<DayOfWeek>(day, true, out _);
    }
}