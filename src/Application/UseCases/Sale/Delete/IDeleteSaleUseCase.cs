namespace Application.UseCases.Sale.Delete;

public interface IDeleteSaleUseCase
{
    public Task<bool> ExecuteAsync(Guid id);
}