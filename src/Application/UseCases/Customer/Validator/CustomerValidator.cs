using Application.DTOs.Customer;
using FluentValidation;

namespace Application.UseCases.Customer.Validator;

public class CustomerValidator : AbstractValidator<CustomerRequest>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty()
            .WithMessage("Customer name is required");
    }
}