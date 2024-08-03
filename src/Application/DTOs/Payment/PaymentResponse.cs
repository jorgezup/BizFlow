namespace Application.DTOs.Payment;

public record PaymentResponse(
    Guid id,
    Guid saleId,
    decimal amount,
    DateTime paymentDate,
    string paymentMethod,
    string paymentStatus,
    DateTime createdAt,
    DateTime updatedAt);