using Application.DTOs.Order;
using FluentValidation;

namespace Application.UseCases.Order.Validator;

public class OrderValidator : AbstractValidator<OrderRequest>
{
    public OrderValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required");
        RuleFor(x => x.OrderDate).NotEmpty().WithMessage("OrderDate is required");
    }
}