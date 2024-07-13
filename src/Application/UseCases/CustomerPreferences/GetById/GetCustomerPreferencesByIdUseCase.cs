using Application.DTOs.CustomerPreferences;
using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.CustomerPreferences.GetById;

public class GetCustomerPreferencesByIdUseCase(IUnitOfWork unitOfWork) : IGetCustomerPreferencesByIdUseCase
{
    public async Task<CustomerPreferencesResponse> ExecuteAsync(Guid id)
    {
        var customerPreferences = await unitOfWork.CustomerPreferencesRepository.GetByIdAsync(id);

        if (customerPreferences is null)
            throw new NotFoundException("Customer preferences not found");

        return customerPreferences.MapToCustomerPreferencesResponse();
    }
}