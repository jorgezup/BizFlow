using Application.DTOs.CustomerPreferences;

namespace Application.UseCases.CustomerPreferences.GetByCustomerId;

public interface IGetCustomerPreferencesByICustomerIdUseCase
{
    public Task<IEnumerable<CustomerPreferencesResponse>> ExecuteAsync(Guid customerId);
}