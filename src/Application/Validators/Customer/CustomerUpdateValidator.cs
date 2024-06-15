using Core.Models.Customer;
using FluentValidation;

namespace Application.Validators.Customer;

public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateRequest>
{
    public CustomerUpdateValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty()
            .WithMessage("Customer name is required");
        RuleFor(customer => customer.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("A valid email address is required");
    }
}