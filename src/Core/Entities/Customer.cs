namespace Core.Entities;

public class Customer(Guid customerId, string name, string email, string phoneNumber, string address)
{
    public Guid CustomerId { get; set; } = customerId;
    public string Name { get; set; } = name;
    public string Email { get; set; } = email;
    public string PhoneNumber { get; set; } = phoneNumber;
    public string Address { get; set; } = address;
}