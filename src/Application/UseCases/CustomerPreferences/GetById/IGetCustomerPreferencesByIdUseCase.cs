using Application.DTOs.CustomerPreferences;

namespace Application.UseCases.CustomerPreferences.GetById;

public interface IGetCustomerPreferencesByIdUseCase
{
    public Task<CustomerPreferencesResponse> ExecuteAsync(Guid id);
}