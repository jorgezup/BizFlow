namespace Application.UseCases.CustomerPreferences.Delete;

public interface IDeleteCustomerPreferences
{
    public Task<bool> ExecuteAsync(Guid id);
}