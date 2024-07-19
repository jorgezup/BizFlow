using Application.DTOs.Payment;

namespace Application.UseCases.Payment.Create;

public interface ICreatePaymentUseCase
{
    public Task<Core.Entities.Payment> ExecuteAsync(PaymentRequest request);
}