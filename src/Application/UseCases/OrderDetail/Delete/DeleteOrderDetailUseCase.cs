using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.OrderDetail.Delete;

public class DeleteOrderDetailUseCase(IUnitOfWork unitOfWork) : IDeleteOrderDetailUseCase
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var saleDetail = await unitOfWork.OrderDetailRepository.GetByIdAsync(id);

            if (saleDetail is null)
                throw new NotFoundException("Sale detail not found");

            await unitOfWork.OrderDetailRepository.DeleteAsync(id);
            await unitOfWork.CommitTransactionAsync();

            return true;
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while deleting the sale detail", ex);
        }
    }
}