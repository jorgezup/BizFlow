namespace Application.DTOs.CustomerPreferences;

public class UpdateCustomerPreferencesRequest(List<string> preferredPurchaseDay)
{
    public required List<string> PreferredPurchaseDays { get; init; } = preferredPurchaseDay;
}