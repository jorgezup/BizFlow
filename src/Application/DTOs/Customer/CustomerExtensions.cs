namespace Application.DTOs.Customer;

public static class CustomerExtensions
{
    public static CustomerResponse MapToCustomerResponse(this Core.Entities.Customer customer)
    {
        return new CustomerResponse(
            customer.CustomerId,
            customer.Name,
            customer.Email,
            customer.PhoneNumber,
            customer.Address,
            customer.CreatedAt,
            customer.UpdatedAt);
    }

    public static Core.Entities.Customer MapToCustomer(this CustomerRequest customerRequest)
    {
        return new Core.Entities.Customer
        {
            CustomerId = Guid.NewGuid(),
            Name = customerRequest.Name,
            Email = customerRequest.Email ?? string.Empty,
            PhoneNumber = customerRequest.PhoneNumber ?? string.Empty,
            Address = customerRequest.Address ?? string.Empty,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static Core.Entities.Customer UpdateCustomer(this Core.Entities.Customer customer,
        CustomerUpdateRequest customerUpdateRequest)
    {
        customer.Name = string.IsNullOrWhiteSpace(customerUpdateRequest.Name)
            ? customer.Name
            : customerUpdateRequest.Name;
        customer.Email = string.IsNullOrWhiteSpace(customerUpdateRequest.Email)
            ? customer.Email
            : customerUpdateRequest.Email;
        customer.PhoneNumber = string.IsNullOrWhiteSpace(customerUpdateRequest.PhoneNumber)
            ? customer.PhoneNumber
            : customerUpdateRequest.PhoneNumber;
        customer.Address = string.IsNullOrWhiteSpace(customerUpdateRequest.Address)
            ? customer.Address
            : customerUpdateRequest.Address;
        customer.UpdatedAt = DateTime.UtcNow;
        return customer;
    }
}