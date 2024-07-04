using Application.DTOs.CustomerPreferences;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.CustomerPreferences.Update;

public class UpdateCustomerPreferences(
    ICustomerPreferencesRepository customerPreferencesRepository,
    IValidator<UpdateCustomerPreferencesRequest> validator) : IUpdateCustomerPreferences
{
    public async Task<CustomerPreferencesResponse?> ExecuteAsync(Guid id, UpdateCustomerPreferencesRequest request)
    {
        var customerPreferences = await customerPreferencesRepository.GetByIdAsync(id);

        if (customerPreferences is null) throw new NotFoundException("Customer preferences not found");

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new DataContractValidationException("Invalid customer preferences data when updating",
                validationResult.Errors);

        customerPreferences.PreferredPurchaseDays = request.PreferredPurchaseDays;
        customerPreferences.UpdatedAt = DateTime.UtcNow;

        await customerPreferencesRepository.UpdateAsync(customerPreferences);

        return customerPreferences.MapToCustomerPreferencesResponse();
    }
}