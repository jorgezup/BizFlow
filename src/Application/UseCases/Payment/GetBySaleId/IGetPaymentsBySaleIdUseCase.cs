namespace Application.UseCases.Payment.GetPaymentBySaleId;

public interface IGetPaymentsBySaleIdUseCase
{
    public Task<IEnumerable<Core.Entities.Payment>> ExecuteAsync(Guid saleId);
}