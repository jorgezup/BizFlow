namespace Core.Models.Customer;

public class CustomerRequest(
    string name,
    string email,
    string phoneNumber,
    string address,
    List<DayOfWeek> preferredDay,
    List<string> preferredProducts)
{
    public string? Name { get; } = name;

    public string Email { get; } = email;

    public string? PhoneNumber { get; } = phoneNumber;

    public string? Address { get; } = address;
    public List<DayOfWeek>? PreferredDay { get; } = preferredDay;
    public List<string>? PreferredProducts { get; } = preferredProducts;
}