using Application.DTOs.CustomerPreferences;
using CustomerPreferencesResponse = Core.DTOs.CustomerPreferencesResponse;

namespace Application.UseCases.CustomerPreferences.GetByCustomerId;

public interface IGetCustomerPreferencesByICustomerIdUseCase
{
    public Task<IEnumerable<CustomerPreferencesResponse>> ExecuteAsync(Guid customerId);
}