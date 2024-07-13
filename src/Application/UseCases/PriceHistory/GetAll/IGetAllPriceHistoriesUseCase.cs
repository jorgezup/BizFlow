using Application.DTOs.PriceHistory;

namespace Application.UseCases.PriceHistory.GetAll;

public interface IGetAllPriceHistoriesUseCase
{
    public Task<IEnumerable<PriceHistoryResponse>> ExecuteAsync();
}