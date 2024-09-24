namespace Application.DTOs.OrderDetail;

public record UpdateOrderDetailRequest(
    Guid ProductId,  
    decimal? Quantity,
    decimal? Subtotal
);