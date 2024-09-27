using Core.Interfaces;
using CustomerPreferencesResponse = Core.DTOs.CustomerPreferencesResponse;

namespace Application.UseCases.CustomerPreferences.GetAll;

public class GetAllCustomerPreferencesUseCase(IUnitOfWork unitOfWork) : IGetAllCustomerPreferencesUseCase
{
    public async Task<IEnumerable<CustomerPreferencesResponse>> ExecuteAsync()
    {
        try
        {
            var customerPreferences = await unitOfWork.CustomerPreferencesRepository.GetAllAsync();

            return customerPreferences;
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while getting customer preferences", e);
        }
    }
}