namespace Application.DTOs.Product;

public static class ProductExtensions
{
    public static ProductResponse MapToProductOutput(this Core.Entities.Product product)
    {
        return new ProductResponse(
            product.ProductId,
            product.Name,
            product.Description,
            product.UnitOfMeasure,
            product.Price,
            product.UpdatedAt,
            product.CreatedAt
        );
    }

    public static Core.Entities.Product MapToProduct(this ProductRequest productRequest)
    {
        return new Core.Entities.Product
        {
            ProductId = Guid.NewGuid(),
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
        product.Name = productUpdateRequest.Name;
        product.Description = string.IsNullOrWhiteSpace(productUpdateRequest.Description)
            ? product.Description
            : productUpdateRequest.Description;
        product.UnitOfMeasure = productUpdateRequest.UnitOfMeasure;
        product.Price = productUpdateRequest.Price;
        product.UpdatedAt = DateTime.UtcNow;

        return product;
    }
}