using Application.DTOs.PriceHistory;

namespace Application.UseCases.PriceHistory.GetById;

public interface IPriceHistoryGetById
{
    public Task<PriceHistoryResponse> ExecuteAsync(Guid id);
}