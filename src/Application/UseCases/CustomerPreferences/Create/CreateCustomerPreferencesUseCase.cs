using Application.DTOs.CustomerPreferences;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.CustomerPreferences.Create;

public class CreateCustomerPreferencesUseCase(
    IUnitOfWork unitOfWork,
    IValidator<CustomerPreferencesRequest> validator)
    : ICreateCustomerPreferencesUseCase
{
    public async Task<CustomerPreferencesResponse> ExecuteAsync(CustomerPreferencesRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new DataContractValidationException("Invalid customer preferences data when creating", validationResult.Errors);

        var customerAndProductExists = await CustomerAndProductExistsAsync(request.CustomerId, request.ProductId);

        if (!customerAndProductExists)
            throw new NotFoundException("Customer or product not found");

        var customerPreferences = request.MapToCustomerPreferences();

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.CustomerPreferencesRepository.AddAsync(customerPreferences);
            await unitOfWork.CommitTransactionAsync();

            return customerPreferences.MapToCustomerPreferencesResponse();
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while creating customer preferences", ex);
        }
    }

    private async Task<bool> CustomerAndProductExistsAsync(Guid customerId, Guid productId)
    {
        var customerExists = await unitOfWork.CustomerRepository.GetByIdAsync(customerId);
        var productExists = await unitOfWork.ProductRepository.GetByIdAsync(productId);
        return customerExists != null && productExists != null;
    }
}