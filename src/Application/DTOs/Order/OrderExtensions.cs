namespace Application.DTOs.Order;

public static class OrderExtensions
{
    public static Core.DTOs.OrderResponse MapToOrderResponse(this Core.Entities.Order order)
    {
        return new Core.DTOs.OrderResponse
        (
            order.Id,
            order.CustomerId,
            order.Customer.Name,
            order.OrderDate,
            TotalAmount: order.OrderDetails.Sum(s => s.Subtotal),
            Products: order.OrderDetails.Select(sd => sd.Product.Name).ToList(),
            Quantity: order.OrderDetails.Select(sd => sd.Quantity).ToList(),
            order.Generated,
            order.OrderLifeCycle.OrderByDescending(x => x.CreatedAt).FirstOrDefault().Status,
            PaymentMethod: string.Empty,
            order.CreatedAt,
            order.UpdatedAt
        );
    }

    public static Core.Entities.Order MapToOrder(this OrderRequest orderRequest)
    {
        return new Core.Entities.Order
        {
            Id = Guid.NewGuid(),
            CustomerId = orderRequest.CustomerId,
            OrderDate = orderRequest.OrderDate,
            Generated = orderRequest.Generated ?? false,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static Core.Entities.Order MapToOrder(this Core.DTOs.OrderResponse response)
    {
        return new Core.Entities.Order
        {
            Id = response.Id,
            CustomerId = response.CustomerId,
            OrderDate = response.OrderDate,
            Generated = response.Generated,
            CreatedAt = response.CreatedAt,
            UpdatedAt = response.UpdatedAt
        };
    }
}