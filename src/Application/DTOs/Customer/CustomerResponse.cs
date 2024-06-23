namespace Application.DTOs.Customer;

public record CustomerResponse(
    Guid CustomerId,
    string Name,
    string? Email,
    string? PhoneNumber,
    string? Address,
    DateTime CreatedAt,
    DateTime UpdatedAt);