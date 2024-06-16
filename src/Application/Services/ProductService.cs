using Application.Interfaces;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models.Product;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class ProductService(
    IProductRepository productRepository,
    ILogger<ProductService> logger,
    IValidator<ProductRequest> validator,
    IValidator<ProductUpdateRequest> validatorUpdateRequest) : IProductService
{
    public async Task<IEnumerable<ProductResponse>?> GetAllAsync()
    {
        try
        {
            var products = await productRepository.GetAllAsync();
            var productsList = products.ToList();

            if (productsList.Count == 0)
            {
                throw new NotFoundException("Product not found");
            }

            return productsList.Select(c => c.MapToProductOutput());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting products");
            throw;
        }
    }

    public async Task<ProductResponse?> GetByIdAsync(Guid id)
    {
        try
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException("Product not found");
            }

            return product.MapToProductOutput();
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while retrieving the product.");
            throw;
        }
    }

    public async Task<ProductResponse> AddAsync(ProductRequest product)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(product);

            if (!validationResult.IsValid)
            {
                throw new DataContractValidationException("Invalid product data", validationResult.Errors);
            }

            var existingProduct = await productRepository.GetByNameAsync(product.Name);
            if (existingProduct is not null)
            {
                throw new ConflictException("Product name already in use");
            }

            var result = await productRepository.AddAsync(product);
            return result.MapToProductOutput();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while adding the product.");
            throw;
        }
    }

    public async Task<ProductResponse?> UpdateAsync(ProductUpdateRequest productUpdateRequest)
    {
        try
        {
            var existingProduct = await productRepository.GetByNameAsync(productUpdateRequest.Name);

            if (existingProduct is null)
            {
                throw new NotFoundException("Product not found");
            }

            var validationResult = await validatorUpdateRequest.ValidateAsync(productUpdateRequest);

            if (!validationResult.IsValid)
            {
                throw new DataContractValidationException("Invalid product data", validationResult.Errors);
            }

            var productToUpdate = existingProduct.UpdateProduct(productUpdateRequest);

            var updatedProduct = await productRepository.UpdateAsync(productToUpdate);

            return updatedProduct.MapToProductOutput();
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while updating the product.");
            throw;
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var product = await productRepository.GetByIdAsync(id);

            if (product is null)
            {
                throw new NotFoundException("Product not found");
            }

            await productRepository.DeleteAsync(id);
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while deleting the product.");
            throw;
        }
    }
}