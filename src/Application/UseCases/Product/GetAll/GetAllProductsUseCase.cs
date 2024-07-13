using Application.DTOs.Product;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Product.GetAll
{
    public class GetAllProductsUseCase(IUnitOfWork unitOfWork) : IGetAllProductsUseCase
    {
        public async Task<IEnumerable<ProductResponse>> ExecuteAsync()
        {
            var products = await unitOfWork.ProductRepository.GetAllAsync();
            var productsList = products.ToList();

            if (productsList.Count is 0)
                throw new NotFoundException("No products found");

            return productsList.Select(c => c.MapToProductResponse());
        }
    }
}