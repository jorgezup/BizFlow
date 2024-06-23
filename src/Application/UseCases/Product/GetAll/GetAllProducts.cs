using Application.DTOs.Product;
using Application.UseCases.Product.GetById;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Product.GetAll;

public class GetAllProducts(IProductRepository productRepository) : IGetAllProducts
{
    public async Task<IEnumerable<ProductResponse>> ExecuteAsync()
    {
        var products = await productRepository.GetAllAsync();
        var productsList = products.ToList();

        if (productsList.Count == 0) throw new NotFoundException("Product not found");

        return productsList.Select(c => c.MapToProductOutput());
    }
}