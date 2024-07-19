using Core.Enums;

namespace Application.UseCases.Payment.Update;

public interface IUpdatePaymentUseCase
{
    public Task<Core.Entities.Payment> ExecuteAsync(Guid paymentId, PaymentStatus status);
}