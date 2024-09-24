using Core.Enums;

namespace Application.DTOs.Payment;

public record PaymentRequest(
    Guid OrderId,
    decimal? Amount,
    DateTime PaymentDate,
    PaymentMethod? PaymentMethod = PaymentMethod.Cash);