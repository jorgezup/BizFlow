using Application.DTOs.CustomerPreferences;
using Core.Interfaces;

namespace Application.UseCases.CustomerPreferences.GetAll;

public class GetAllCustomerPreferencesUseCase(IUnitOfWork unitOfWork) : IGetAllCustomerPreferencesUseCase
{
    public async Task<IEnumerable<CustomerPreferencesResponse>> ExecuteAsync()
    {
        try
        {
            var customerPreferences = await unitOfWork.CustomerPreferencesRepository.GetAllAsync();

            return customerPreferences.Select(x => x.MapToCustomerPreferencesResponse());
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while getting customer preferences", e);
        }
    }
}