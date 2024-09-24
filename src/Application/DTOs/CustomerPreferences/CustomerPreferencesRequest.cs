namespace Application.DTOs.CustomerPreferences;

public class CustomerPreferencesRequest(
    Guid customerId,
    Guid productId,
    decimal quantity,
    string preferredPurchaseDay)
{
    public Guid CustomerId { get; } = customerId;
    public Guid ProductId { get; } = productId;
    public required decimal Quantity { get; init; } = quantity;
    public required string PreferredPurchaseDay { get; init; } = preferredPurchaseDay;
}