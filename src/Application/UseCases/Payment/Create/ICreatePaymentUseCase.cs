using Application.DTOs.Payment;
using PaymentResponse = Core.DTOs.PaymentResponse;

namespace Application.UseCases.Payment.Create;

public interface ICreatePaymentUseCase
{
    public Task<PaymentResponse> ExecuteAsync(PaymentRequest request);
}