using Application.DTOs.PriceHistory;

namespace Application.UseCases.PriceHistory.GetById;

public interface IGetPriceHistoryByIdUseCase
{
    public Task<PriceHistoryResponse> ExecuteAsync(Guid id);
}