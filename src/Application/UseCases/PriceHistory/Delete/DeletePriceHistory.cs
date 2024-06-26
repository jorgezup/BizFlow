using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.PriceHistory.Delete;

public class DeletePriceHistory(IPriceHistoryRepository priceHistoryRepository) : IDeletePriceHistory
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        var priceHistory = await priceHistoryRepository.GetByIdAsync(id);

        if (priceHistory == null) throw new NotFoundException("Price history not found");

        await priceHistoryRepository.DeleteAsync(id);
        return true;
    }
}