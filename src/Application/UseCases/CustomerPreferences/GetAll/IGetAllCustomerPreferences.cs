using Application.DTOs.CustomerPreferences;

namespace Application.UseCases.CustomerPreferences.GetAll;

public interface IGetAllCustomerPreferences
{
    public Task<IEnumerable<CustomerPreferencesResponse>> ExecuteAsync();
}