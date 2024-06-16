namespace Core.Models.Product;

public record ProductResponse(
    Guid ProductId,
    string Name,
    decimal Price,
    string? Category,
    DateTime UpdatedAt);