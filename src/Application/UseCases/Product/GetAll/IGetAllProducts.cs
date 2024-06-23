using Application.DTOs.Product;

namespace Application.UseCases.Product.GetById;

public interface IGetAllProducts
{
    public Task<IEnumerable<ProductResponse>> ExecuteAsync();
}