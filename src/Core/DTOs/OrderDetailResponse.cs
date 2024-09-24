namespace Core.DTOs;

public record OrderDetailResponse(
    Guid Id,
    Guid OrderId,
    Guid ProductId,
    Guid CustomerId,
    string CustomerName,
    string ProductName,
    decimal Quantity,
    decimal UnitPrice,
    decimal Subtotal,
    DateTime CreatedAt,
    DateTime UpdatedAt);