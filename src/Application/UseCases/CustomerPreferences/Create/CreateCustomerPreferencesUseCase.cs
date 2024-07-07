using Application.DTOs.CustomerPreferences;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.CustomerPreferences.Create;

public class CreateCustomerPreferencesUseCase(
    ICustomerPreferencesRepository customerPreferencesRepository,
    IValidator<CustomerPreferencesRequest> validator,
    ICustomerRepository customerRepository,
    IProductRepository productRepository) : ICreateCustomerPreferencesUseCase
{
    public async Task<CustomerPreferencesResponse> ExecuteAsync(CustomerPreferencesRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);
        
        var customerAndProductExists = await CustomerAndProductExistsAsync(request.CustomerId, request.ProductId);
        
        if (!customerAndProductExists)
            throw new NotFoundException("Customer or product not found");
        
        if (!validationResult.IsValid)
            throw new DataContractValidationException("Invalid customer preferences data when creating",
                validationResult.Errors);

        var customerPreferences = request.MapToCustomerPreferences();

        await customerPreferencesRepository.AddAsync(customerPreferences);

        return customerPreferences.MapToCustomerPreferencesResponse();
    }

    private async Task<bool> CustomerAndProductExistsAsync(Guid requestCustomerId, Guid requestProductId)
    {
        var customerExists = await customerRepository
            .GetByIdAsync(requestCustomerId);
        var productExists = await productRepository
            .GetByIdAsync(requestProductId);
        return customerExists is not null && productExists is not null;
    }
}