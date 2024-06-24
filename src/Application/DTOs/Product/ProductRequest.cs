namespace Application.DTOs.Product;

public class ProductRequest(
    string name,
    string? description,
    string unitOfMeasure,
    decimal price)
{
    public required string Name { get; init; } = name;
    public string? Description { get; } = description;
    public required string UnitOfMeasure { get; init; } = unitOfMeasure;
    public required decimal Price { get; init; } = price;
}