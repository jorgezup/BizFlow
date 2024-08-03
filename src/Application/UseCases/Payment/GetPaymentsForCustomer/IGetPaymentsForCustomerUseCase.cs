using Application.DTOs.Payment;

namespace Application.UseCases.Payment.GetTotalPaymentsForCustomer;

public interface IGetPaymentsForCustomerUseCase
{
    public Task<IEnumerable<PaymentsForCustomerResponse>> ExecuteAsync(Guid customerId);
}