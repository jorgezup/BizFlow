using Core.Enums;

namespace Application.DTOs.Payment;

public record PaymentRequest(
    Guid SaleId,
    decimal Amount,
    PaymentMethod Method,
    PaymentStatus Status,
    string TransactionId);