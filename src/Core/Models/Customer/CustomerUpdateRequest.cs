namespace Core.Models.Customer;

public record CustomerUpdateRequest(
    string Name,
    string Email,
    string? PhoneNumber,
    string? Address,
    List<DayOfWeek>? PreferredDay,
    List<string>? PreferredProducts);