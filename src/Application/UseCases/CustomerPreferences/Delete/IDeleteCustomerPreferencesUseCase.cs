namespace Application.UseCases.CustomerPreferences.Delete;

public interface IDeleteCustomerPreferencesUseCase
{
    public Task<bool> ExecuteAsync(Guid id);
}