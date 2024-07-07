namespace Application.UseCases.PriceHistory.Delete;

public interface IDeletePriceHistoryUseCase
{
    public Task<bool> ExecuteAsync(Guid id);
}