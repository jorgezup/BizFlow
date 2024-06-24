using Application.DTOs.PriceHistory;

namespace Application.UseCases.PriceHistory.Create;

public interface ICreatePriceHistory
{
    public Task<PriceHistoryResponse> ExecuteAsync(PriceHistoryRequest request);
}