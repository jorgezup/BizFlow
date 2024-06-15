namespace Core.Models.Customer;

public static class CustomerExtensions
{
    public static CustomerResponse MapToCustomerOutput(this Entities.Customer customer)
    {
        return new CustomerResponse(customer.CustomerId, customer.Name, customer.Email, customer.PhoneNumber,
            customer.Address, customer.PreferredProducts, customer.PreferredDay)
        {
            CustomerId = customer.CustomerId,
            Name = customer.Name,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Address = customer.Address,
            PreferredProducts = customer.PreferredProducts ?? [],
            PreferredDay = customer.PreferredDay ?? []
        };
    }
    
    public static Entities.Customer MapToCustomer(this CustomerRequest customerRequest)
    {
        return new Entities.Customer
        {
            CustomerId = Guid.NewGuid(),
            Name = customerRequest.Name ?? string.Empty,
            Email = customerRequest.Email ?? string.Empty,
            PhoneNumber = customerRequest.PhoneNumber ?? string.Empty,
            Address = customerRequest.Address ?? string.Empty,
            PreferredDay = customerRequest.PreferredDay,
            PreferredProducts = customerRequest.PreferredProducts,
            UpdatedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public static Entities.Customer UpdateCustomer(this Entities.Customer customer, CustomerUpdateRequest customerUpdateRequest)
    {
        customer.Name = customerUpdateRequest.Name;
        customer.Email = customerUpdateRequest.Email;
        customer.PhoneNumber = string.IsNullOrWhiteSpace(customerUpdateRequest.PhoneNumber) ? customer.PhoneNumber : customerUpdateRequest.PhoneNumber;
        customer.Address = string.IsNullOrWhiteSpace(customerUpdateRequest.Address) ? customer.Address : customerUpdateRequest.Address;
        customer.PreferredDay = customerUpdateRequest.PreferredDay ?? customer.PreferredDay; // maybe a frontend validation should be added 
        customer.PreferredProducts = customerUpdateRequest.PreferredProducts ?? customer.PreferredProducts; // maybe a frontend validation should be added 
        customer.UpdatedAt = DateTime.UtcNow;
        return customer;
    }
}