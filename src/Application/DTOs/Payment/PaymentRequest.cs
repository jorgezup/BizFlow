using Core.Enums;

namespace Application.DTOs.Payment;

public record PaymentRequest(
    Guid SaleId,
    decimal Amount,
    string Method,
    DateTime PaymentDate);