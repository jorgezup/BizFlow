using Application.DTOs.Product;

namespace Application.UseCases.Product.GetAll;

public interface IGetAllProducts
{
    public Task<IEnumerable<ProductResponse>> ExecuteAsync();
}