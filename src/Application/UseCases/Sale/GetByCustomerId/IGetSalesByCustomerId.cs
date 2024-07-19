namespace Application.UseCases.Sale.GetByCustomerId;

public interface IGetSalesByCustomerId
{
    public Task<IEnumerable<Core.Entities.Sale>> ExecuteAsync(Guid customerId);
}