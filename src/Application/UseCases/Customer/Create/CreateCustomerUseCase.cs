using Application.DTOs.Customer;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.Customer.Create;

public class CreateCustomerUseCase(
    IUnitOfWork unitOfWork,
    IValidator<CustomerRequest> validator)
    : ICreateCustomerUseCase
{
    public async Task<CustomerResponse> ExecuteAsync(CustomerRequest request)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            throw new DataContractValidationException("Invalid customer data when creating",
                validationResult.Errors);

        await unitOfWork.BeginTransactionAsync();

        try
        {
            var customerFound = await unitOfWork.CustomerRepository.GetByEmailAsync(request.Email);
            if (customerFound != null && !string.IsNullOrWhiteSpace(customerFound.Email))
                throw new ConflictException("Email already in use when creating");

            var customer = request.MapToCustomer();

            await unitOfWork.CustomerRepository.AddAsync(customer);
            await unitOfWork.CommitTransactionAsync();

            return customer.MapToCustomerResponse();
        }
        catch (Exception ex) when (ex is not DataContractValidationException and not ConflictException)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while creating the customer", ex);
        }
    }
}