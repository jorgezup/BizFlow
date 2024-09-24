using Application.DTOs.Payment;
using PaymentResponse = Core.DTOs.PaymentResponse;

namespace Application.UseCases.Payment.Update;

public interface IUpdatePaymentUseCase
{
    public Task<PaymentResponse> ExecuteAsync(Guid paymentId, PaymentUpdateRequest request);
}