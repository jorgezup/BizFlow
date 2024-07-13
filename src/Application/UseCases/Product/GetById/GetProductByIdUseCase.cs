using Application.DTOs.Product;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Product.GetById
{
    public class GetProductByIdUseCase(IUnitOfWork unitOfWork) : IGetProductByIdUseCase
    {
        public async Task<ProductResponse?> ExecuteAsync(Guid id)
        {
            var product = await unitOfWork.ProductRepository.GetByIdAsync(id);

            if (product is null)
                throw new NotFoundException("Product not found");

            return product.MapToProductResponse();
        }
    }
}