namespace Application.DTOs.Payment;

public record PaymentUpdateRequest(
    Guid OrderId,
    decimal? Amount,
    DateTime? PaymentDate,
    string? PaymentMethod);