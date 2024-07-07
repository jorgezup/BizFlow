using Application.DTOs.PriceHistory;
using Core.Interfaces;

namespace Application.UseCases.PriceHistory.Create;

public class CreatePriceHistoryUseCase(IPriceHistoryRepository priceHistoryRepository) : ICreatePriceHistoryUseCase
{
    public async Task<PriceHistoryResponse> ExecuteAsync(PriceHistoryRequest request)
    {
        var priceHistory = new Core.Entities.PriceHistory
        {
            ProductId = request.ProductId,
            Price = request.Price
        };

        await priceHistoryRepository.AddAsync(priceHistory);

        return priceHistory.MapToPriceHistoryResponse();
    }
}