namespace Core.Models.Product;

public class ProductRequest(
    string name,
    decimal price,
    string category)
{
    public string Name { get; } = name;

    public decimal Price { get; } = price;

    public string? Category { get; } = category;
}