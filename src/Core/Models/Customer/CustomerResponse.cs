namespace Core.Models.Customer;

public record CustomerResponse(
    Guid CustomerId,
    string Name,
    string Email,
    string? PhoneNumber,
    string? Address,
    List<string>? PreferredProducts,
    List<DayOfWeek>? PreferredDay);