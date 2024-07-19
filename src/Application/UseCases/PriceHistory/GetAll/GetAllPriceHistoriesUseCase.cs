using Application.DTOs.PriceHistory;
using Core.Interfaces;

namespace Application.UseCases.PriceHistory.GetAll;

public class GetAllPriceHistoriesUseCase(IUnitOfWork unitOfWork) : IGetAllPriceHistoriesUseCase
{
    public async Task<IEnumerable<PriceHistoryResponse>> ExecuteAsync()
    {
        try
        {
            var priceHistories = await unitOfWork.PriceHistoryRepository.GetAllAsync();
            return priceHistories.Select(x => x.MapToPriceHistoryResponse());
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while getting all price histories", e);
        }
    }
}