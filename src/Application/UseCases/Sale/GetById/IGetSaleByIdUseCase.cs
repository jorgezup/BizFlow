using Application.DTOs.Sale;

namespace Application.UseCases.Sale.GetById;

public interface IGetSaleByIdUseCase
{
    public Task<SaleResponse> ExecuteAsync(Guid id);
}