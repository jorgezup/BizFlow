using Application.DTOs.PriceHistory;
using Core.Interfaces;

namespace Application.UseCases.PriceHistory.Create;

public class CreatePriceHistoryUseCase(IUnitOfWork unitOfWork) : ICreatePriceHistoryUseCase
{
    public async Task<PriceHistoryResponse> ExecuteAsync(PriceHistoryRequest request)
    {
        var priceHistory = new Core.Entities.PriceHistory
        {
            ProductId = request.ProductId,
            Price = request.Price
        };

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.PriceHistoryRepository.AddAsync(priceHistory);
            await unitOfWork.CommitTransactionAsync();

            return priceHistory.MapToPriceHistoryResponse();
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while creating price history", ex);
        }
    }
}