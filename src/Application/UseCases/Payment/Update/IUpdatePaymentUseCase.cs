using Application.DTOs.Payment;

namespace Application.UseCases.Payment.Update;

public interface IUpdatePaymentUseCase
{
    public Task<PaymentResponse> ExecuteAsync(Guid paymentId, PaymentUpdateRequest request);
}