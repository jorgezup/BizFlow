using Application.DTOs.CustomerPreferences;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.CustomerPreferences.GetById;

public class GetCustomerPreferencesByIdUseCase(ICustomerPreferencesRepository customerPreferencesRepository) : IGetCustomerPreferencesByIdUseCase
{
    public async Task<CustomerPreferencesResponse> ExecuteAsync(Guid id)
    {
        var customerPreferences = await customerPreferencesRepository.GetByIdAsync(id);

        if (customerPreferences == null) throw new NotFoundException("Customer preferences not found");

        return customerPreferences.MapToCustomerPreferencesResponse();
    }
}