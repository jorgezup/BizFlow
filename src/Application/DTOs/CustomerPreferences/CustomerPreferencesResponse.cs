namespace Application.DTOs.CustomerPreferences;

public record CustomerPreferencesResponse(
    Guid Id,
    Guid CustomerId,
    Guid ProductId,
    List<string> PreferredPurchaseDays,
    DateTime CreatedAt,
    DateTime UpdatedAt);