using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.CustomerPreferences.GetByCustomerId;

public class GetCustomerPreferencesByICustomerIdUseCase(IUnitOfWork unitOfWork) : IGetCustomerPreferencesByICustomerIdUseCase
{
    public async Task<IEnumerable<Core.Entities.CustomerPreferences>> ExecuteAsync(Guid customerId)
    {
        var customerPreferencesByCustomerId = await unitOfWork.CustomerPreferencesRepository.GetByCustomerIdAsync(customerId);

        var customerPreferencesByCustomerIdList = customerPreferencesByCustomerId.ToList();
        if (customerPreferencesByCustomerIdList.Count is 0)
            throw new NotFoundException("Customer preferences not found");
        
        return customerPreferencesByCustomerIdList;
    }
}