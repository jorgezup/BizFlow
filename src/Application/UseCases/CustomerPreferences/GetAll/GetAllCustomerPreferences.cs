using Application.DTOs.CustomerPreferences;
using Core.Interfaces;

namespace Application.UseCases.CustomerPreferences.GetAll;

public class GetAllCustomerPreferences(ICustomerPreferencesRepository customerPreferencesRepository)
    : IGetAllCustomerPreferences
{
    public async Task<IEnumerable<CustomerPreferencesResponse>> ExecuteAsync()
    {
        var customerPreferences = await customerPreferencesRepository.GetAllAsync();

        return customerPreferences.Select(x => x.MapToCustomerPreferencesResponse());
    }
}