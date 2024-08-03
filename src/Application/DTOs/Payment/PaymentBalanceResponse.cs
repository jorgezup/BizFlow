namespace Application.DTOs.Payment;

public record PaymentBalanceResponse(
    Guid saleId,
    decimal paid,
    decimal remaining);