namespace Application.DTOs.Product;

public static class ProductExtensions
{
    public static ProductResponse MapToProductOutput(this Core.Entities.Product product)
    {
        return new ProductResponse(
            product.Id,
            product.Name,
            product.Description,
            product.UnitOfMeasure,
            product.Price,
            product.CreatedAt,
            product.UpdatedAt);
    }

    public static Core.Entities.Product MapToProduct(this ProductRequest productRequest)
    {
        return new Core.Entities.Product
        {
            Id = Guid.NewGuid(),
            Name = productRequest.Name,
            Description = productRequest.Description ?? string.Empty,
            UnitOfMeasure = productRequest.UnitOfMeasure,
            Price = productRequest.Price,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Core.Entities.Product UpdateProduct(this Core.Entities.Product product,
        ProductUpdateRequest productUpdateRequest)
    {
        product.Name = string.IsNullOrWhiteSpace(productUpdateRequest.Name)
            ? product.Name
            : productUpdateRequest.Name;
        product.Description = string.IsNullOrWhiteSpace(productUpdateRequest.Description)
            ? product.Description
            : productUpdateRequest.Description;
        product.UnitOfMeasure = (string.IsNullOrWhiteSpace(productUpdateRequest.UnitOfMeasure)
            ? product.UnitOfMeasure
            : productUpdateRequest.UnitOfMeasure)!;
        product.Price = (decimal)(productUpdateRequest.Price == 0
            ? product.Price
            : productUpdateRequest.Price)!;
        product.UpdatedAt = DateTime.UtcNow;

        return product;
    }
}