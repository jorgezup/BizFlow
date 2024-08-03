using Application.DTOs.PriceHistory;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.PriceHistory.GetByProductId;

public class GetPriceHistoryByProductIdUseCase(IUnitOfWork unitOfWork) : IGetPriceHistoryByProductIdUseCase
{
    public async Task<IEnumerable<PriceHistoryResponse>> ExecuteAsync(Guid productId)
    {
        try
        {
            var priceHistories = (await unitOfWork.PriceHistoryRepository.GetByProductIdAsync(productId)).ToList();

            if (priceHistories.Count is 0)
                throw new NotFoundException("Price history not found");

            return priceHistories
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => x.MapToPriceHistoryResponse());
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting price histories by product id", e);
        }
    }
}