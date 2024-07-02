using Application.DTOs.Product;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.Product.Create;

public class CreateProduct(
    IProductRepository productRepository,
    IPriceHistoryRepository priceHistoryRepository,
    IValidator<ProductRequest> validator) : ICreateProduct
{
    public async Task<ProductResponse> ExecuteAsync(ProductRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new DataContractValidationException("Invalid product data", validationResult.Errors);

        var existingProduct = await productRepository.GetByNameAsync(request.Name);
        if (existingProduct is not null) throw new ConflictException("Product name already in use");

        var product = request.MapToProduct();

        var priceHistory = new Core.Entities.PriceHistory
        {
            ProductId = product.ProductId,
            Price = product.Price
        };

        await productRepository.AddAsync(product);
        
        // Add price history
        await priceHistoryRepository.AddAsync(priceHistory);

        return product.MapToProductOutput();
    }
}