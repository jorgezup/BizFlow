namespace Application.UseCases.Customer.Delete;

public interface IDeleteCustomerUseCase
{
    public Task<bool> ExecuteAsync(Guid id);
}