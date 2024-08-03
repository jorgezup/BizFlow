using Application.DTOs.Payment;

namespace Application.UseCases.Payment.GetById;

public interface IGetPaymentByIdUseCase
{
    public Task<PaymentResponse> ExecuteAsync(Guid paymentId);
}