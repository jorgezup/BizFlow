namespace Application.DTOs.Payment;

public record PaymentUpdateRequest(
    decimal? amount,
    DateTime? paymentDate,
    string? paymentMethod,
    string? status);