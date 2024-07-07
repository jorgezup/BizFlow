using Application.DTOs.CustomerPreferences;
using FluentValidation;

namespace Application.UseCases.CustomerPreferences.Validator;

public class CustomerPreferencesValidator : AbstractValidator<CustomerPreferencesRequest>
{
    public CustomerPreferencesValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Id is required.");
        RuleForEach(x => x.PreferredPurchaseDays)
            .NotEmpty().WithMessage("PreferredPurchaseDays is required.")
            .Must(BeAValidDayOfWeek).WithMessage("PreferredPurchaseDays must be a valid day of the week.");
    }

    private bool BeAValidDayOfWeek(string day)
    {
        return Enum.TryParse<DayOfWeek>(day, true, out _);
    }
}