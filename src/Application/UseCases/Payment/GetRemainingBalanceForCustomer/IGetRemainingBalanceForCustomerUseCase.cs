namespace Application.UseCases.Payment.GetRemainingBalanceForCustomer;

public interface IGetRemainingBalanceForCustomerUseCase
{
    public Task<decimal> ExecuteAsync(Guid customerId);
}