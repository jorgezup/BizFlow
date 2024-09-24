using Application.DTOs.Product;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Product.GetAll;

public class GetAllProductsUseCase(IUnitOfWork unitOfWork) : IGetAllProductsUseCase
{
    public async Task<IEnumerable<ProductResponse>> ExecuteAsync()
    {
        try
        {
            var products = await unitOfWork.ProductRepository.GetAllAsync();

            return products.Select(c => c.MapToProductResponse());
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while creating the customer", ex);
        }
    }
}