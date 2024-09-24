namespace Core.DTOs;

public record PaymentResponse(
    Guid Id,
    Guid OrderId,
    Guid CustomerId,
    string CustomerName,
    decimal Amount,
    DateTime PaymentDate,
    string PaymentMethod,
    DateTime CreatedAt,
    DateTime UpdatedAt);    