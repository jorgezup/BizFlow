using Application.DTOs.PriceHistory;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.PriceHistory.Update;

public class UpdatePriceHistoryUseCase(IPriceHistoryRepository priceHistoryRepository) : IUpdatePriceHistoryUseCase
{
    public async Task<PriceHistoryResponse> ExecuteAsync(Guid id, UpdatePriceHistoryRequest request)
    {
        var priceHistory = await priceHistoryRepository.GetByIdAsync(id);

        if (priceHistory == null) throw new NotFoundException("Price history not found");

        priceHistory.Price = request.Price;

        await priceHistoryRepository.UpdateAsync(priceHistory);

        return priceHistory.MapToPriceHistoryResponse();
    }
}