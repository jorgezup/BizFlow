using Application.DTOs.Customer;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.Customer.Update;

public class UpdateCustomerUseCase(
    ICustomerRepository customerRepository,
    IValidator<CustomerUpdateRequest> validator) : IUpdateCustomerUseCase
{
    public async Task<CustomerResponse> ExecuteAsync(Guid id, CustomerUpdateRequest request)
    {
        var customerFound = await customerRepository.GetByIdAsync(id);

        if (customerFound is null) throw new NotFoundException("Customer not found when updating");

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new DataContractValidationException("Invalid customer data when updating",
                    validationResult.Errors);
            
            var checkEmail = await customerRepository.GetByEmailAsync(request.Email);
            if (checkEmail is not null)
            {
                if ( checkEmail.Id != customerFound.Id)
                    throw new ConflictException("Email already in use when updating");
                
            }
        }

        var customerToUpdate = customerFound.UpdateCustomer(request);

        var updatedCustomer = await customerRepository.UpdateAsync(customerToUpdate);

        return updatedCustomer.MapToCustomerResponse();
    }
}