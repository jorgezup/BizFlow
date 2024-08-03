using Application.DTOs.Payment;

namespace Application.UseCases.Payment.GetRemainingBalanceForCustomer;

public interface IGetRemainingBalanceForCustomerUseCase
{
    public Task<IEnumerable<PaymentBalanceResponse>> ExecuteAsync(Guid customerId);
}