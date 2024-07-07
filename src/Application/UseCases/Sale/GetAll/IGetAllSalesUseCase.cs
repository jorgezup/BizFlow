using Application.DTOs.Sale;

namespace Application.UseCases.Sale.GetAll;

public interface IGetAllSalesUseCase
{
    public Task<IEnumerable<SaleResponse>> ExecuteAsync();
}