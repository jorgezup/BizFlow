using Application.DTOs.Product;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.Product.Update;

public class UpdateProduct(
    IProductRepository productRepository,
    IValidator<ProductUpdateRequest> validatorUpdateRequest) : IUpdateProduct
{
    public async Task<ProductResponse> ExecuteAsync(Guid productId, ProductUpdateRequest productUpdateRequest)
    {
        var existingProduct = await productRepository.GetByIdAsync(productId);

        if (existingProduct is null) throw new NotFoundException("Product not found");

        // var validationResult = await validatorUpdateRequest.ValidateAsync(productUpdateRequest);
        //
        // if (!validationResult.IsValid)
        // {
        //     throw new DataContractValidationException("Invalid product data", validationResult.Errors);
        // }

        var productToUpdate = existingProduct.UpdateProduct(productUpdateRequest);
        await productRepository.UpdateAsync(productToUpdate);
        return productToUpdate.MapToProductOutput();
    }
}