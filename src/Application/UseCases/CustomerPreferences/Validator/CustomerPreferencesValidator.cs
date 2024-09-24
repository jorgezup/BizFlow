using Application.DTOs.CustomerPreferences;
using FluentValidation;

namespace Application.UseCases.CustomerPreferences.Validator;

public class CustomerPreferencesValidator : AbstractValidator<CustomerPreferencesRequest>
{
    public CustomerPreferencesValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("Id is required.");
        RuleFor(x => x.PreferredPurchaseDay)
            .NotEmpty().WithMessage("PreferredPurchaseDay is required.")
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