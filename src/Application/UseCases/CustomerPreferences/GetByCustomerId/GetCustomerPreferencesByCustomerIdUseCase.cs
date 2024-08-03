using Application.DTOs.CustomerPreferences;
using Core.Exceptions;
using Core.Interfaces;

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
            if (customerPreferencesByCustomerIdList.Count is 0)
                throw new NotFoundException("Customer preferences not found");

            return customerPreferencesByCustomerIdList.Select(x => x.MapToCustomerPreferencesResponse());
        }
        catch (Exception e) when (e is not NotFoundException)
        {
            throw new ApplicationException("An error occurred while getting customer preferences by customer id", e);
        }
    }
}