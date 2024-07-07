using Application.DTOs.PriceHistory;

namespace Application.UseCases.PriceHistory.Create;

public interface ICreatePriceHistoryUseCase
{
    public Task<PriceHistoryResponse> ExecuteAsync(PriceHistoryRequest request);
}