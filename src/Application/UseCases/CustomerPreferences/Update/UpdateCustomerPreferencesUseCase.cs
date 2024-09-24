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
        var customerPreference = await unitOfWork.CustomerPreferencesRepository.GetByIdAsync(id);

        if (customerPreference == null)
            throw new NotFoundException("Customer preferences not found");

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new DataContractValidationException("Invalid customer preferences data when updating",
                validationResult.Errors);

        customerPreference.PreferredPurchaseDay = request.PreferredPurchaseDays;
        customerPreference.Quantity = request.Quantity;
        customerPreference.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.CustomerPreferencesRepository.UpdateAsync(customerPreference);
            await unitOfWork.CommitTransactionAsync();
            
            customerPreference.Customer = await unitOfWork.CustomerRepository.GetByIdAsync(customerPreference.CustomerId);
            customerPreference.Product = await unitOfWork.ProductRepository.GetByIdAsync(customerPreference.ProductId);

            return customerPreference.MapToCustomerPreferencesResponse();
        }
        catch (Exception ex) when (ex is not DataContractValidationException and not NotFoundException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while updating customer preferences", ex);
        }
    }
}