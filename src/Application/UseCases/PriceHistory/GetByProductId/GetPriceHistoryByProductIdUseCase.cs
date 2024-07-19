using Application.DTOs.PriceHistory;
using Core.Interfaces;

namespace Application.UseCases.PriceHistory.GetByProductId;

public class GetPriceHistoryByProductIdUseCase(IUnitOfWork unitOfWork) : IGetPriceHistoryByProductIdUseCase
{
    public async Task<IEnumerable<PriceHistoryResponse>> ExecuteAsync(Guid productId)
    {
        try
        {
            var priceHistories = await unitOfWork.PriceHistoryRepository.GetByProductIdAsync(productId);
            return priceHistories.Select(x => x.MapToPriceHistoryResponse());
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while getting price histories by product id", e);
        }
    }
}