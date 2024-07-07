using Application.DTOs.CustomerPreferences;

namespace Application.UseCases.CustomerPreferences.Update;

public interface IUpdateCustomerPreferencesUseCase
{
    public Task<CustomerPreferencesResponse?> ExecuteAsync( Guid id, UpdateCustomerPreferencesRequest request);
}