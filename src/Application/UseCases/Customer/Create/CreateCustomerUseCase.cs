using Application.DTOs.Customer;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.Customer.Create;

public class CreateCustomerUseCase(
    ICustomerRepository customerRepository,
    IValidator<CustomerRequest> validator) : ICreateCustomerUseCase
{
    public async Task<CustomerResponse> ExecuteAsync(CustomerRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new DataContractValidationException("Invalid customer data when creating",
                validationResult.Errors);

        var customerFound = await customerRepository.GetByEmailAsync(request.Email);
        if (customerFound is not null) throw new ConflictException("Email already in use when creating");

        var customer = request.MapToCustomer();

        await customerRepository.AddAsync(customer);

        return customer.MapToCustomerResponse();
    }
}