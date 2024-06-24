namespace Application.DTOs.PriceHistory;

public class PriceHistoryResponse
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; }
}