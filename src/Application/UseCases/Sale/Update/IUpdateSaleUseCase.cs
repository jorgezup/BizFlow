using Application.DTOs.Sale;

namespace Application.UseCases.Sale.Update;

public interface IUpdateSaleUseCase
{
    public Task<SaleResponse> ExecuteAsync(Guid id, UpdateSaleRequest request);
}