using Core.Exceptions;
using Core.Interfaces;
using CustomerPreferencesResponse = Core.DTOs.CustomerPreferencesResponse;

namespace Application.UseCases.CustomerPreferences.GetById;

public class GetCustomerPreferencesByIdUseCase(IUnitOfWork unitOfWork) : IGetCustomerPreferencesByIdUseCase
{
    public async Task<CustomerPreferencesResponse> ExecuteAsync(Guid id)
    {
        try
        {
            var customerPreference = await unitOfWork.CustomerPreferencesRepository.GetByIdAsync(id);

            if (customerPreference is null)
                throw new NotFoundException("Customer preferences not found");

            return customerPreference;
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting customer preferences by id", e);
        }
    }
}