using Application.DTOs.Product;

namespace Application.UseCases.Product.GetById;

public interface IGetProductByIdUseCase
{
    public Task<ProductResponse?> ExecuteAsync(Guid id);
}