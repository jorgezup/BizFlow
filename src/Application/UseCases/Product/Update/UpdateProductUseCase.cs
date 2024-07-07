using Application.DTOs.Product;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Product.Update;

public class UpdateProductUseCase(
    IProductRepository productRepository,
    IPriceHistoryRepository priceHistoryRepository) : IUpdateProductUseCase
{
    public async Task<ProductResponse> ExecuteAsync(Guid productId, ProductUpdateRequest productUpdateRequest)
    {
        var existingProduct = await productRepository.GetByIdAsync(productId);

        if (existingProduct is null) throw new NotFoundException("Product not found");

        if (productUpdateRequest.Price is < 0 or 0) throw new BadRequestException("Price cannot be negative");

        if (existingProduct.Price != productUpdateRequest.Price)
        {
            var priceHistory = new Core.Entities.PriceHistory
            {
                ProductId = productId,
                Price = (decimal)productUpdateRequest.Price!
            };

            await priceHistoryRepository.AddAsync(priceHistory);
        }

        var productToUpdate = existingProduct.UpdateProduct(productUpdateRequest);

        await productRepository.UpdateAsync(productToUpdate);

        return productToUpdate.MapToProductOutput();
    }
}