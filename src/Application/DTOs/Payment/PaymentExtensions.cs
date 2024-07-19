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
            PaymentDate = DateTime.UtcNow,
            Status = request.Status,
            PaymentMethod = request.Method,
            TransactionId = request.TransactionId
        };
    }
}