using Application.DTOs.CustomerPreferences;

namespace Application.UseCases.CustomerPreferences.Create;

public interface ICreateCustomerPreferences
{
    public Task<CustomerPreferencesResponse> ExecuteAsync(CustomerPreferencesRequest request);
}