namespace Core.Entities;

public class SaleItem(Guid saleItemId, Guid saleId, Sale sale, Guid productId, Product product, int quantity, decimal price)
{
    public Guid SaleItemId { get; set; } = Guid.NewGuid();
    public Guid SaleId { get; set; } = saleId;
    public Sale Sale { get; set; } = sale;
    public Guid ProductId { get; set; } = productId;
    public Product Product { get; set; } = product;
    public int Quantity { get; set; } = quantity;
    public decimal Price { get; set; } = price;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}