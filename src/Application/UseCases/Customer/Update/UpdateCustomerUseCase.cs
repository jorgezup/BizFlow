using Application.DTOs.Customer;
using Core.Exceptions;
using Core.Interfaces;
using FluentValidation;

namespace Application.UseCases.Customer.Update;

public class UpdateCustomerUseCase(IUnitOfWork unitOfWork, IValidator<CustomerUpdateRequest> validator)
    : IUpdateCustomerUseCase
{
    public async Task<CustomerResponse> ExecuteAsync(Guid id, CustomerUpdateRequest request)
    {
        var customerFound = await unitOfWork.CustomerRepository.GetByIdAsync(id);

        if (customerFound == null)
            throw new NotFoundException("Customer not found");

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
                throw new DataContractValidationException("Invalid customer data", validationResult.Errors);

            var checkEmail = await unitOfWork.CustomerRepository.GetByEmailAsync(request.Email);
            if (checkEmail != null && checkEmail.Id != customerFound.Id)
                throw new ConflictException("Email already in use");
        }

        var customerToUpdate = customerFound.UpdateCustomer(request);

        await unitOfWork.BeginTransactionAsync();

        try
        {
            await unitOfWork.CustomerRepository.UpdateAsync(customerToUpdate);
            await unitOfWork.CommitTransactionAsync();
            return customerToUpdate.MapToCustomerResponse();
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackTransactionAsync();
            throw new ApplicationException("An error occurred while updating the customer", ex);
        }
    }
}