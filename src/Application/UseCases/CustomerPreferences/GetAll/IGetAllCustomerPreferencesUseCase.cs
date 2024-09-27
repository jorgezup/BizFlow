using Application.DTOs.CustomerPreferences;
using CustomerPreferencesResponse = Core.DTOs.CustomerPreferencesResponse;

namespace Application.UseCases.CustomerPreferences.GetAll;

public interface IGetAllCustomerPreferencesUseCase
{
    public Task<IEnumerable<CustomerPreferencesResponse>> ExecuteAsync();
}