using Core.Enums;

namespace Application.DTOs.Payment;

public static class PaymentExtensions
{
    public static Core.Entities.Payment MapToPayment(this PaymentRequest request)
    {
        return new Core.Entities.Payment
        {
            Id = Guid.NewGuid(),
            SaleId = request.SaleId,
            Amount = request.Amount,
            PaymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), request.Method),
            PaymentDate = request.PaymentDate,
            Status = PaymentStatus.Completed,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static PaymentResponse MapToPaymentResponse(this Core.Entities.Payment payment)
    {
        return new PaymentResponse(
            payment.Id,
            payment.SaleId,
            payment.Amount,
            payment.PaymentDate,
            payment.PaymentMethod.ToString(),
            payment.Status.ToString(),
            payment.CreatedAt,
            payment.UpdatedAt);
    }
    
    
    public static void UpdatePayment(this Core.Entities.Payment payment, PaymentUpdateRequest request)
    {
        payment.Amount = request.amount ?? payment.Amount;
        payment.Status = !string.IsNullOrWhiteSpace(request.status) ? (PaymentStatus)Enum.Parse(typeof(PaymentStatus), request.status) : payment.Status;
        payment.PaymentMethod = !string.IsNullOrWhiteSpace(request.paymentMethod) ? (PaymentMethod)Enum.Parse(typeof(PaymentMethod), request.paymentMethod) : payment.PaymentMethod;
        payment.PaymentDate = request.paymentDate ?? payment.PaymentDate;
        payment.UpdatedAt = DateTime.UtcNow;
    }
}