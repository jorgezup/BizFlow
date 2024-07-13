using Application.DTOs.CustomerPreferences;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.CustomerPreferences.Update;

public class UpdateCustomerPreferencesUseCase(
    IUnitOfWork unitOfWork,
    IValidator<UpdateCustomerPreferencesRequest> validator)
    : IUpdateCustomerPreferencesUseCase
{
    public async Task<CustomerPreferencesResponse> ExecuteAsync(Guid id, UpdateCustomerPreferencesRequest request)
    {
        var customerPreferences = await unitOfWork.CustomerPreferencesRepository.GetByIdAsync(id);

        if (customerPreferences == null)
            throw new NotFoundException("Customer preferences not found");

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new DataContractValidationException("Invalid customer preferences data when updating",
                validationResult.Errors);

        customerPreferences.PreferredPurchaseDays = request.PreferredPurchaseDays;
        customerPreferences.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.CustomerPreferencesRepository.UpdateAsync(customerPreferences);
            await unitOfWork.CommitTransactionAsync();

            return customerPreferences.MapToCustomerPreferencesResponse();
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while updating customer preferences", ex);
        }
    }
}