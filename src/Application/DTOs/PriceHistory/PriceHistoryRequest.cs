namespace Application.DTOs.PriceHistory;

public class PriceHistoryRequest
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
}