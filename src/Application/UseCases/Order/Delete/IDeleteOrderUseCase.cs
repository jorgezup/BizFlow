namespace Application.UseCases.Order.Delete;

public interface IDeleteOrderUseCase
{
    public Task ExecuteAsync(Guid id);
}