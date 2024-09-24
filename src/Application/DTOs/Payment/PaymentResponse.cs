namespace Application.DTOs.Payment;

public record PaymentResponse(
    Guid Id,
    Guid OrderId,
    Guid CustomerId,
    decimal Amount,
    DateTime PaymentDate,
    string PaymentMethod,
    DateTime CreatedAt,
    DateTime UpdatedAt);