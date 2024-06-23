namespace Application.UseCases.Customer.Delete;

public interface IDeleteCustomer
{
    public Task<bool> ExecuteAsync(Guid id);
}