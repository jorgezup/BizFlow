using Core.Enums;

namespace Application.DTOs.Order;

public record OrderResponse(
    Guid Id,
    Guid CustomerId,
    string CustomerName,
    DateTime OrderDate,
    decimal TotalAmount,
    List<string> Products,
    List<decimal> Quantity,
    bool Generated,
    Status Status,
    DateTime CreatedAt,
    DateTime UpdatedAt
    );