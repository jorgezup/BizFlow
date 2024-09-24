namespace Application.DTOs.CustomerPreferences;

public record CustomerPreferencesResponse(
    Guid Id,
    Guid CustomerId,
    string CustomerName,
    Guid ProductId,
    string ProductName,
    decimal Quantity,
    string PreferredPurchaseDay,
    DateTime CreatedAt,
    DateTime UpdatedAt);