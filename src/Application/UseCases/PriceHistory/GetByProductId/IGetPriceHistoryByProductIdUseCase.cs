using Application.DTOs.PriceHistory;

namespace Application.UseCases.PriceHistory.GetByProductId;

public interface IGetPriceHistoryByProductIdUseCase
{
    public Task<IEnumerable<PriceHistoryResponse>> ExecuteAsync(Guid productId);
}