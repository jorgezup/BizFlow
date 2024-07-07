using Application.DTOs.Product;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Product.GetById;

public class GetProductByIdUseCase(IProductRepository productRepository) : IGetProductByIdUseCase
{
    public async Task<ProductResponse?> ExecuteAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product is null) throw new NotFoundException("Product not found");

        return product.MapToProductOutput();
    }
}