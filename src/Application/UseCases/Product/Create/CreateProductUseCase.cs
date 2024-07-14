using Application.DTOs.Product;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.Product.Create;

public class CreateProductUseCase(
    IUnitOfWork unitOfWork,
    IValidator<ProductRequest> validator) : ICreateProductUseCase
{
    public async Task<ProductResponse> ExecuteAsync(ProductRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new DataContractValidationException("Invalid product data", validationResult.Errors);

        var existingProduct = await unitOfWork.ProductRepository.GetByNameAsync(request.Name);
        if (existingProduct is not null) throw new ConflictException("Product name already in use");

        await unitOfWork.BeginTransactionAsync();
        
        try
        {
            var product = request.MapToProduct();

            var priceHistory = new Core.Entities.PriceHistory
            {
                ProductId = product.Id,
                Price = product.Price,
                Product = product
            };

            await unitOfWork.ProductRepository.AddAsync(product);

            // Add price history
            await unitOfWork.PriceHistoryRepository.AddAsync(priceHistory);
            
            await unitOfWork.CommitTransactionAsync();

            return product.MapToProductResponse();
        }
        catch (Exception e) when (e is not ConflictException and not DataContractValidationException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while creating the product", e);
        }
    }
}