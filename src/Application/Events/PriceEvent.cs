using MediatR;

namespace Application.Events;

public class PriceEvent(Guid productId, decimal price) : INotification
{
    public Guid ProductId { get; set; } = productId;
    public decimal Price { get; set; } = price;
}