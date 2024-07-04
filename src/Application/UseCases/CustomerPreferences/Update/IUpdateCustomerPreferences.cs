using Application.DTOs.CustomerPreferences;

namespace Application.UseCases.CustomerPreferences.Update;

public interface IUpdateCustomerPreferences
{
    public Task<CustomerPreferencesResponse?> ExecuteAsync( Guid id, UpdateCustomerPreferencesRequest request);
}