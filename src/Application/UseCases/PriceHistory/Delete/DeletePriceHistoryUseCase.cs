using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.PriceHistory.Delete;

public class DeletePriceHistoryUseCase(IUnitOfWork unitOfWork) : IDeletePriceHistoryUseCase
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        var priceHistory = await unitOfWork.PriceHistoryRepository.GetByIdAsync(id);

        if (priceHistory is null)
            throw new NotFoundException("Price history not found");

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.PriceHistoryRepository.DeleteAsync(id);
            await unitOfWork.CommitTransactionAsync();
            return true;
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while deleting price history", ex);
        }
    }
}