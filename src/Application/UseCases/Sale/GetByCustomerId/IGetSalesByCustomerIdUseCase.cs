using Application.DTOs.Sale;

namespace Application.UseCases.Sale.GetByCustomerId;

public interface IGetSalesByCustomerIdUseCase
{
    public Task<IEnumerable<SaleResponse>> ExecuteAsync(Guid customerId);
}