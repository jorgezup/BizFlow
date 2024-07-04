using Core.Exceptions;
using Core.Interfaces;

namespace Application.UseCases.CustomerPreferences.Delete;

public class DeleteCustomerPreferences(ICustomerPreferencesRepository customerPreferencesRepository) : IDeleteCustomerPreferences
{
    public async Task<bool> ExecuteAsync(Guid id)
    {
        var customerPreferences = await customerPreferencesRepository.GetByIdAsync(id);

        if (customerPreferences == null) throw new NotFoundException("Customer preferences not found");

        await customerPreferencesRepository.DeleteAsync(id);
        return true;
    }
}