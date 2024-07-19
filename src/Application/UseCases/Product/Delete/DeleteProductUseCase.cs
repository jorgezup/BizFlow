using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.Product.Delete;

public class DeleteProductUseCase(IUnitOfWork unitOfWork) : IDeleteProductUseCase
{
    public async Task<bool> ExecuteAsync(Guid productId)
    {
        var product = await unitOfWork.ProductRepository.GetByIdAsync(productId);

        if (product is null)
            throw new NotFoundException("Product not found");

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.ProductRepository.DeleteAsync(productId);
            await unitOfWork.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while deleting the product", ex);
        }
    }
}