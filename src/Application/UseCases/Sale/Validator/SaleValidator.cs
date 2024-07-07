using Application.DTOs.Sale;
using FluentValidation;

namespace Application.UseCases.Sale.Validator;

public class SaleValidator : AbstractValidator<SaleRequest>
{
    public SaleValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
        RuleFor(x => x.SaleDate).NotEmpty().WithMessage("SaleDate is required.");
    }
}