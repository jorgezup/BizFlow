using Application.DTOs.Product;

namespace Application.UseCases.Product.Create;

public interface ICreateProductUseCase
{
    public Task<ProductResponse> ExecuteAsync(ProductRequest product);
}