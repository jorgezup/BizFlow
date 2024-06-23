namespace Application.DTOs.Customer;

public record CustomerUpdateRequest(
    Guid CustomerId,
    string Name,
    string? Email,
    string? PhoneNumber,
    string? Address);