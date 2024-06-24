namespace Application.UseCases.PriceHistory.Delete;

public interface IDeletePriceHistory
{
    public Task<bool> ExecuteAsync(Guid id);
}