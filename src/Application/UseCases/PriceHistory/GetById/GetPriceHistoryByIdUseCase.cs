using Application.DTOs.PriceHistory;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.PriceHistory.GetById;

public class GetPriceHistoryByIdUseCase(IUnitOfWork unitOfWork) : IGetPriceHistoryByIdUseCase
{
    public async Task<PriceHistoryResponse> ExecuteAsync(Guid id)
    {
        var priceHistory = await unitOfWork.PriceHistoryRepository.GetByIdAsync(id);

        if (priceHistory is null)
            throw new NotFoundException("Price history not found");

        return priceHistory.MapToPriceHistoryResponse();
    }
}