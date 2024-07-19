using Application.DTOs.Product;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Product.Update;

public class UpdateProductUseCase(IUnitOfWork unitOfWork) : IUpdateProductUseCase
{
    public async Task<ProductResponse> ExecuteAsync(Guid productId, ProductUpdateRequest productUpdateRequest)
    {
        var existingProduct = await unitOfWork.ProductRepository.GetByIdAsync(productId);

        if (existingProduct is null)
            throw new NotFoundException("Product not found");

        if (productUpdateRequest.Price <= 0)
            throw new BadRequestException("Price must be greater than zero");

        if (existingProduct.Price == productUpdateRequest.Price)
            throw new ConflictException("Price cannot be the same as the current price");

        var priceHistory = new Core.Entities.PriceHistory
        {
            ProductId = productId,
            Price = (decimal)productUpdateRequest.Price!
        };

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.PriceHistoryRepository.AddAsync(priceHistory);
            existingProduct.UpdateProduct(productUpdateRequest);
            await unitOfWork.ProductRepository.UpdateAsync(existingProduct);
            await unitOfWork.CommitTransactionAsync();

            return existingProduct.MapToProductResponse();
        }
        catch (Exception ex) when (ex is not NotFoundException and not BadRequestException and not ConflictException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while updating the product", ex);
        }
    }
}