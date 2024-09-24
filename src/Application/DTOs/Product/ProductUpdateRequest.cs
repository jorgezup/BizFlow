namespace Application.DTOs.Product;

public record ProductUpdateRequest(
    // string? Name,
    string? Description,
    string? UnitOfMeasure,
    decimal? Price);