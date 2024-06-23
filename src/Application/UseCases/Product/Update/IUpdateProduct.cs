using Application.DTOs.Product;

namespace Application.UseCases.Product.Update;

public interface IUpdateProduct
{
    public Task<ProductResponse> ExecuteAsync(Guid productId, ProductUpdateRequest productUpdateRequest);
}