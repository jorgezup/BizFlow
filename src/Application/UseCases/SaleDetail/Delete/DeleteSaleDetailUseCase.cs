using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.SaleDetail.Delete;

public class DeleteSaleDetailUseCase(IUnitOfWork unitOfWork) : IDeleteSaleDetailUseCase
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        await unitOfWork.BeginTransactionAsync();

        try
        {
            var saleDetail = await unitOfWork.SaleDetailRepository.GetByIdAsync(id);

            if (saleDetail is null)
                throw new NotFoundException("Sale detail not found");

            await unitOfWork.SaleDetailRepository.DeleteAsync(id);
            await unitOfWork.CommitTransactionAsync();

            return true;
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while deleting the sale detail", ex);
        }
    }
}