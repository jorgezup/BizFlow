using Core.Models.Product;
using FluentValidation;

namespace Application.Validators.Product;

public class ProductUpdateValidator : AbstractValidator<ProductUpdateRequest>
{
    public ProductUpdateValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage("Product name is required");
        RuleFor(product => product.Price)
            .NotEmpty()
            .WithMessage("Product price is required");
    }
}