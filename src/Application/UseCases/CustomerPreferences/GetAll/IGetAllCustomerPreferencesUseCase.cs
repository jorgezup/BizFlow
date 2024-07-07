using Application.DTOs.CustomerPreferences;

namespace Application.UseCases.CustomerPreferences.GetAll;

public interface IGetAllCustomerPreferencesUseCase
{
    public Task<IEnumerable<CustomerPreferencesResponse>> ExecuteAsync();
}