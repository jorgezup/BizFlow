using Application.DTOs.Customer;
using FluentValidation;

namespace Application.Validators.Customer;

public class CustomerValidator : AbstractValidator<CustomerRequest>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty()
            .WithMessage("Customer name is required");
    }
}