namespace Core.Entities;

public class Product(Guid productId, string name, decimal price, string category, List<SaleItem> saleItems)
{
    public Guid ProductId { get; set; } = productId;
    public string Name { get; set; } = name;
    public decimal Price { get; set; } = price;
    public string Category { get; set; } = category;
    public List<SaleItem> SaleItems { get; set; } = saleItems;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}