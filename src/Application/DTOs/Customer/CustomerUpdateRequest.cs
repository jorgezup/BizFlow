namespace Application.DTOs.Customer;

public record CustomerUpdateRequest(
    string? Name,
    string? Email,
    string? PhoneNumber,
    string? Address);