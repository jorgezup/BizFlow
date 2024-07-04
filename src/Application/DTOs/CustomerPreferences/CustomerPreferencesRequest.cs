namespace Application.DTOs.CustomerPreferences;

public class CustomerPreferencesRequest(
    Guid customerId,
    Guid productId,
    List<string> preferredPurchaseDays)
{
    public Guid CustomerId { get; } = customerId;
    public Guid ProductId { get; } = productId;
    public required List<string> PreferredPurchaseDays { get; init; } = preferredPurchaseDays;
}