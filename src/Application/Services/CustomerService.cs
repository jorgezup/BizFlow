using Application.Interfaces;
using Core.Exceptions;
using Core.Interfaces;
using Core.Models.Customer;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.Services;

public class CustomerService(
    ICustomerRepository customerRepository,
    ILogger<CustomerService> logger,
    IValidator<CustomerRequest> validator,
    IValidator<CustomerUpdateRequest> validatorUpdateRequest) : ICustomerService
{
    public async Task<IEnumerable<CustomerResponse>?> GetAllAsync()
    {
        try
        {
            var customers = await customerRepository.GetAllAsync();
            var customersList = customers.ToList();

            if (customersList.Count == 0)
            {
                throw new NotFoundException("Customer not found");
            }

            return customersList.Select(c => c.MapToCustomerOutput());
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error getting customers");
            throw;
        }
    }

    public async Task<CustomerResponse?> GetByIdAsync(Guid id)
    {
        try
        {
            var customer = await customerRepository.GetByIdAsync(id);

            if (customer is null)
            {
                throw new NotFoundException("Customer not found");
            }

            return customer.MapToCustomerOutput();
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while retrieving the customer.");
            throw;
        }
    }

    public async Task<CustomerResponse> AddAsync(CustomerRequest customer)
    {
        try
        {
            var validationResult = await validator.ValidateAsync(customer);

            if (!validationResult.IsValid)
            {
                throw new DataContractValidationException("Invalid customer data", validationResult.Errors);
            }

            var existingCustomer = await customerRepository.GetByEmailAsync(customer.Email);
            if (existingCustomer is not null)
            {
                throw new ConflictException("Email already in use");
            }

            var result = await customerRepository.AddAsync(customer);
            return result.MapToCustomerOutput();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while adding the customer.");
            throw;
        }
    }

    public async Task<CustomerResponse?> UpdateAsync(CustomerUpdateRequest customerUpdateRequest)
    {
        try
        {
            var existingCustomer = await customerRepository.GetByEmailAsync(customerUpdateRequest.Email);

            if (existingCustomer is null)
            {
                throw new NotFoundException("Customer not found");
            }

            var validationResult = await validatorUpdateRequest.ValidateAsync(customerUpdateRequest);

            if (!validationResult.IsValid)
            {
                throw new DataContractValidationException("Invalid customer data", validationResult.Errors);
            }

            var customerToUpdate = existingCustomer.UpdateCustomer(customerUpdateRequest);

            var updatedCustomer = await customerRepository.UpdateAsync(customerToUpdate);

            return updatedCustomer.MapToCustomerOutput();
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while updating the customer.");
            throw;
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var customer = await customerRepository.GetByIdAsync(id);

            if (customer is null)
            {
                throw new NotFoundException("Customer not found");
            }

            await customerRepository.DeleteAsync(id);
        }
        catch (NotFoundException)
        {
            throw;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred while deleting the customer.");
            throw;
        }
    }
}