namespace Application.DTOs.PriceHistory;

public static class PriceHistoryExtensions
{
    public static PriceHistoryResponse MapToPriceHistoryResponse(this Core.Entities.PriceHistory priceHistory)
    {
        return new PriceHistoryResponse
        {
            Id = priceHistory.Id,
            ProductId = priceHistory.ProductId,
            Price = priceHistory.Price,
            CreatedAt = priceHistory.CreatedAt
        };
    }
}