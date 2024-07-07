using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Product.Delete;

public class DeleteProductUseCase(IProductRepository productRepository) : IDeleteProductUseCase
{
    public async Task<bool> ExecuteAsync(Guid productId)
    {
        var product = await productRepository.GetByIdAsync(productId);

        if (product == null) throw new NotFoundException("Product not found");

        await productRepository.DeleteAsync(productId);
        return true;
    }
}