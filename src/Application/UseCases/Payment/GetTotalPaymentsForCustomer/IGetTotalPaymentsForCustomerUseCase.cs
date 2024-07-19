namespace Application.UseCases.Payment.GetTotalPaymentsForCustomer;

public interface IGetTotalPaymentsForCustomerUseCase
{
    public Task<decimal> ExecuteAsync(Guid customerId);
}