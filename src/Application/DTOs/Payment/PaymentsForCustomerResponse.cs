using Core.Enums;

namespace Application.DTOs.Payment;

public record PaymentsForCustomerResponse(
    Guid saleId,
    Guid paymentId,
    string paymentMethod,
    DateTime paymentDate,
    decimal amount);