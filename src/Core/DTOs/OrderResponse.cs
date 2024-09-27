using Core.Enums;

namespace Core.DTOs;

public record OrderResponse(
    Guid Id,
    Guid CustomerId,
    string CustomerName,
    DateTime OrderDate,
    decimal TotalAmount,
    List<string> Products,
    List<decimal> Quantity,
    List<decimal> UnitPrice,
    List<decimal> Subtotal,
    bool Generated,
    Status Status,
    string PaymentMethod,
    DateTime CreatedAt,
    DateTime UpdatedAt
);