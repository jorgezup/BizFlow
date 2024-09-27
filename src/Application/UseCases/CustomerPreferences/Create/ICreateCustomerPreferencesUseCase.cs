using Application.DTOs.CustomerPreferences;
using CustomerPreferencesResponse = Core.DTOs.CustomerPreferencesResponse;

namespace Application.UseCases.CustomerPreferences.Create;

public interface ICreateCustomerPreferencesUseCase
{
    public Task<CustomerPreferencesResponse> ExecuteAsync(CustomerPreferencesRequest request);
}