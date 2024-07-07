using Application.DTOs.CustomerPreferences;

namespace Application.UseCases.CustomerPreferences.Create;

public interface ICreateCustomerPreferencesUseCase
{
    public Task<CustomerPreferencesResponse> ExecuteAsync(CustomerPreferencesRequest request);
}