namespace Core.Models.Product;

public record ProductUpdateRequest(
    string Name,
    decimal Price,
    string? Category);