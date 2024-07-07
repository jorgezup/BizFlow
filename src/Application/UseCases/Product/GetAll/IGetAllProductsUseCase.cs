using Application.DTOs.Product;

namespace Application.UseCases.Product.GetAll;

public interface IGetAllProductsUseCase
{
    public Task<IEnumerable<ProductResponse>> ExecuteAsync();
}