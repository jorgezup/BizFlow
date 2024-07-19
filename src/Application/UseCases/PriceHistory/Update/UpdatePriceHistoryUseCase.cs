using Application.DTOs.PriceHistory;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.PriceHistory.Update;

public class UpdatePriceHistoryUseCase(IUnitOfWork unitOfWork) : IUpdatePriceHistoryUseCase
{
    public async Task<PriceHistoryResponse> ExecuteAsync(Guid id, UpdatePriceHistoryRequest request)
    {
        var priceHistory = await unitOfWork.PriceHistoryRepository.GetByIdAsync(id);

        if (priceHistory is null)
            throw new NotFoundException("Price history not found");

        priceHistory.Price = request.Price;

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.PriceHistoryRepository.UpdateAsync(priceHistory);
            await unitOfWork.CommitTransactionAsync();

            return priceHistory.MapToPriceHistoryResponse();
        }
        catch (Exception ex) when (ex is not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while updating price history", ex);
        }
    }
}