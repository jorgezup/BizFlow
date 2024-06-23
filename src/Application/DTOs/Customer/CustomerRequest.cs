namespace Application.DTOs.Customer;

public class CustomerRequest(
    string name,
    string? email,
    string phoneNumber,
    string? address)
{
    public string Name { get; } = name;

    public string? Email { get; } = email;

    public string? PhoneNumber { get; } = phoneNumber;

    public string? Address { get; } = address;
}