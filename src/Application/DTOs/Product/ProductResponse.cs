namespace Application.DTOs.Product;

public record ProductResponse(
    Guid ProductId,
    string Name,
    string? Description,
    string UnitOfMeasure,
    decimal Price,
    DateTime UpdatedAt,
    DateTime CreatedAt);