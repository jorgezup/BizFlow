using Application.DTOs.Product;

namespace Application.UseCases.Product.GetById;

public interface IGetProductById
{
    public Task<ProductResponse?> ExecuteAsync(Guid id);
}