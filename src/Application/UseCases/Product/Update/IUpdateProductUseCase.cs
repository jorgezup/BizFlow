using Application.DTOs.Product;

namespace Application.UseCases.Product.Update;

public interface IUpdateProductUseCase
{
    public Task<ProductResponse> ExecuteAsync(Guid productId, ProductUpdateRequest productUpdateRequest);
}