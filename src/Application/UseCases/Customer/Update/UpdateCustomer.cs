using Application.DTOs.Customer;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.Customer.Update;

public class UpdateCustomer(
    ICustomerRepository customerRepository,
    IValidator<CustomerUpdateRequest> validatorUpdateRequest) : IUpdateCustomer
{
    public async Task<CustomerResponse> ExecuteAsync(Guid id, CustomerUpdateRequest request)
    {
        var customerFound = await customerRepository.GetByIdAsync(id);

        if (customerFound is null) throw new NotFoundException("Customer not found when updating");

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var checkEmail = await customerRepository.GetByEmailAsync(request.Email);
            if (checkEmail is not null && checkEmail.CustomerId != customerFound.CustomerId)
                throw new ConflictException("Email already in use when updating");
        }

        // var validationResult = await validatorUpdateRequest.ValidateAsync(request);
        //
        // if (!validationResult.IsValid)
        // {
        //     throw new DataContractValidationException("Invalid customer data when updating", validationResult.Errors);
        // }

        var customerToUpdate = customerFound.UpdateCustomer(request);

        var updatedCustomer = await customerRepository.UpdateAsync(customerToUpdate);

        return updatedCustomer.MapToCustomerResponse();
    }
}