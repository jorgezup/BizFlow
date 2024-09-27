using CustomerPreferencesResponse = Core.DTOs.CustomerPreferencesResponse;

namespace Application.UseCases.CustomerPreferences.GetById;

public interface IGetCustomerPreferencesByIdUseCase
{
    public Task<CustomerPreferencesResponse> ExecuteAsync(Guid id);
}