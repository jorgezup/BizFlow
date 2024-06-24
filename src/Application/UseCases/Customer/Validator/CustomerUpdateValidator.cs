using Application.DTOs.Customer;
using FluentValidation;

namespace Application.UseCases.Customer.Validator;

public class CustomerUpdateValidator : AbstractValidator<CustomerUpdateRequest>
{
    public CustomerUpdateValidator()
    {
        RuleFor(customer => customer.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email address");
    }
}