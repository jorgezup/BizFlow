namespace Core.Entities;

public class SaleDetail
{
    public Guid Id { get; init; }
    public Guid SaleId { get; init; }
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdatedAt { get; set; }

    public Sale Sale { get; init; } = null!;
    public Product Product { get; init; } = null!;
}