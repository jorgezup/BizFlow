using Application.DTOs.PriceHistory;

namespace Application.UseCases.PriceHistory.Update;

public interface IUpdatePriceHistory
{
    public Task<PriceHistoryResponse> ExecuteAsync(Guid id, UpdatePriceHistoryRequest request);
}