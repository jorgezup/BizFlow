using Application.DTOs.Product;
using Application.Events;
using Core.Exceptions;
using Core.Interfaces;
using MediatR;

namespace Application.UseCases.Product.Update;

public class UpdateProductUseCase(IUnitOfWork unitOfWork, IMediator mediator) : IUpdateProductUseCase
{
    public async Task<ProductResponse> ExecuteAsync(Guid productId, ProductUpdateRequest productUpdateRequest)
    {
        var existingProduct = await unitOfWork.ProductRepository.GetByIdAsync(productId);

        if (existingProduct is null)
            throw new NotFoundException("Product not found");

        if (productUpdateRequest.Price <= 0)
            throw new BadRequestException("Price must be greater than zero");

        await unitOfWork.BeginTransactionAsync();

        try
        {
            if (productUpdateRequest.Price is not null)
            {
                await mediator.Publish(new PriceEvent(productId, productUpdateRequest.Price.Value)); 
            }
            
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