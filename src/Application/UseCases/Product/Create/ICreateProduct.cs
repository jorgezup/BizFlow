using Application.DTOs.Product;

namespace Application.UseCases.Product.Create;

public interface ICreateProduct
{
    public Task<ProductResponse> ExecuteAsync(ProductRequest product);
}