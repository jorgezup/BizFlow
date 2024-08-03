using Application.DTOs.Payment;

namespace Application.UseCases.Payment.Create;

public interface ICreatePaymentUseCase
{
    public Task<PaymentResponse> ExecuteAsync(PaymentRequest request);
}