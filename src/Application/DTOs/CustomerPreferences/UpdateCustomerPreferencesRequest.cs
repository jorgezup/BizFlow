namespace Application.DTOs.CustomerPreferences;

public record UpdateCustomerPreferencesRequest
{
    public required List<string> PreferredPurchaseDays { get; set; } = [];
}