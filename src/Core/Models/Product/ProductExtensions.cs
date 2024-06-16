namespace Core.Models.Product;

public static class ProductExtensions
{
    public static ProductResponse MapToProductOutput(this Entities.Product product)
    {
        return new ProductResponse(product.ProductId, product.Name, product.Price, product.Category, product.UpdatedAt)
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Price = product.Price,
            Category = product.Category,
            UpdatedAt = product.UpdatedAt
        };
    }
    
    public static Entities.Product MapToProduct(this ProductRequest productRequest)
    {
        return new Entities.Product
        {
            ProductId = Guid.NewGuid(),
            Name = productRequest.Name,
            Price = productRequest.Price,
            Category = productRequest.Category ?? string.Empty,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public static Entities.Product UpdateProduct(this Entities.Product product, ProductUpdateRequest productUpdateRequest)
    {
        product.Name = productUpdateRequest.Name;
        product.Price = productUpdateRequest.Price;
        product.Category = string.IsNullOrWhiteSpace(productUpdateRequest.Category) ? product.Category : productUpdateRequest.Category;
        product.UpdatedAt = DateTime.UtcNow;
        return product;
    }
}