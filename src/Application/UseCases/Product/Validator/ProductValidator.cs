using Application.DTOs.Product;
using FluentValidation;

namespace Application.UseCases.Product.Validator;

public class ProductValidator : AbstractValidator<ProductRequest>
{
    public ProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage("Product name is required");
        RuleFor(product => product.UnitOfMeasure)
            .NotEmpty()
            .WithMessage("Product unit of measure is required");
        RuleFor(product => product.Price)
            .NotEmpty()
            .WithMessage("Product price is required");
    }
}