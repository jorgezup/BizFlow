using Core.Exceptions;
using Core.Interfaces;
using CustomerPreferencesResponse = Core.DTOs.CustomerPreferencesResponse;

namespace Application.UseCases.CustomerPreferences.GetByCustomerId;

public class GetCustomerPreferencesByCustomerIdUseCase(IUnitOfWork unitOfWork)
    : IGetCustomerPreferencesByICustomerIdUseCase
{
    public async Task<IEnumerable<CustomerPreferencesResponse>> ExecuteAsync(Guid customerId)
    {
        try
        {
            var customerPreferencesByCustomerId =
                await unitOfWork.CustomerPreferencesRepository.GetByCustomerIdAsync(customerId);
            
            var customerPreferencesByCustomerIdList = customerPreferencesByCustomerId.ToList();
            
            if (customerPreferencesByCustomerId is null || customerPreferencesByCustomerId.Count() is 0)
                return [];

            return customerPreferencesByCustomerIdList;
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting customer preferences by customer id", e);
        }
    }
}