using Application.DTOs.Payment;

namespace Application.UseCases.Payment.GetPendingPayments;

public interface IGetPendingPaymentsUseCase
{
    Task<PendingPaymentResponse> ExecuteAsync(Guid? customerId, DateTime? startDate, DateTime? endDate);
}