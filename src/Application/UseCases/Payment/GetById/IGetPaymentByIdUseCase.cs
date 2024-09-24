using Core.DTOs;

namespace Application.UseCases.Payment.GetById;

public interface IGetPaymentByIdUseCase
{
    public Task<PaymentResponse> ExecuteAsync(Guid paymentId);
}