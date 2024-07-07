using Application.DTOs.PriceHistory;

namespace Application.UseCases.PriceHistory.Update;

public interface IUpdatePriceHistoryUseCase
{
    public Task<PriceHistoryResponse> ExecuteAsync(Guid id, UpdatePriceHistoryRequest request);
}