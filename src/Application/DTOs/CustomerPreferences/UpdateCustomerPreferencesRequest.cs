namespace Application.DTOs.CustomerPreferences;

public record UpdateCustomerPreferencesRequest
{
    public required string PreferredPurchaseDays { get; set; }

    public required decimal Quantity { get; set; }
}