using Application.DTOs.CustomerPreferences;

namespace Application.UseCases.CustomerPreferences.GetById;

public interface IGetCustomerPreferencesById
{
    public Task<CustomerPreferencesResponse> ExecuteAsync(Guid id);
}