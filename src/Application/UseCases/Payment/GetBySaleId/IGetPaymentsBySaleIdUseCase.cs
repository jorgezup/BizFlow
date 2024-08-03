using Application.DTOs.Payment;

namespace Application.UseCases.Payment.GetPaymentBySaleId;

public interface IGetPaymentsBySaleIdUseCase
{
    public Task<IEnumerable<PaymentResponse>> ExecuteAsync(Guid saleId);
}