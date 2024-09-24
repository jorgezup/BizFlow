namespace Application.DTOs.Payment;

public static class PaymentExtensions
{
    public static Core.Entities.Payment MapToPayment(this PaymentRequest request)
    {
        return new Core.Entities.Payment
        {
            Id = Guid.NewGuid(),
            OrderId = request.OrderId,
            // Amount = request.Amount ?? 0,
            // PaymentMethod = (PaymentMethod)Enum.Parse(typeof(PaymentMethod), request.PaymentMethod.ToString()),
            PaymentDate = request.PaymentDate,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
        };
    }

    public static Core.DTOs.PaymentResponse MapToPaymentResponse(this Core.Entities.Payment payment)
    {
        return new  Core.DTOs.PaymentResponse(
            payment.Id,
            payment.OrderId,
            payment.Order.Customer.Id,
            payment.Order.Customer.Name,
            payment.Amount,
            payment.PaymentDate,
            payment.PaymentMethod.ToString(),
            payment.CreatedAt,
            payment.UpdatedAt);
    }
    
}