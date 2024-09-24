using Application.DTOs.OrderDetail;
using FluentValidation;

namespace Application.UseCases.OrderDetail.Validator;

public class OrderDetailValidator : AbstractValidator<OrderDetailRequest>
{
    public OrderDetailValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("OrderId is required.");
        RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        RuleFor(x => x.UnitPrice).GreaterThanOrEqualTo(0).WithMessage("UnitPrice must be greater than or equal to 0.");
        RuleFor(x => x.Subtotal).GreaterThanOrEqualTo(0).WithMessage("Subtotal must be greater than or equal to 0.");
    }
}