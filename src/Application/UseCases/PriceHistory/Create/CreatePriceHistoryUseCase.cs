using Application.DTOs.PriceHistory;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.PriceHistory.Create;

public class CreatePriceHistoryUseCase(IUnitOfWork unitOfWork) : ICreatePriceHistoryUseCase
{
    public async Task<PriceHistoryResponse> ExecuteAsync(PriceHistoryRequest request)
    {
        var productExists = await unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
        if (productExists is null)
            throw new NotFoundException("Product not found");
        
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
        catch (Exception ex) when (ex is not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while creating price history", ex);
        }
    }
}