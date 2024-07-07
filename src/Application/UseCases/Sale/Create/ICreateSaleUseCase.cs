using Application.DTOs.Sale;

namespace Application.UseCases.Sale.Create;

public interface ICreateSaleUseCase
{
    public Task<SaleResponse> ExecuteAsync(SaleRequest request);
}