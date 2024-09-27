namespace Application.DTOs.CustomerPreferences;

public static class CustomerPreferencesExtensions
{
    public static Core.DTOs.CustomerPreferencesResponse MapToCustomerPreferencesResponse(
        this Core.Entities.CustomerPreferences customerPreferences)
    {
        return new Core.DTOs.CustomerPreferencesResponse(
            customerPreferences.Id,
            customerPreferences.CustomerId,
            customerPreferences.Customer.Name,
            customerPreferences.ProductId,
            customerPreferences.Product.Name,
            customerPreferences.Quantity,
            customerPreferences.PreferredPurchaseDay,
            customerPreferences.CreatedAt,
            customerPreferences.UpdatedAt);
    }

    public static Core.Entities.CustomerPreferences MapToCustomerPreferences(
        this CustomerPreferencesRequest customerPreferencesRequest)
    {
        return new Core.Entities.CustomerPreferences
        {
            CustomerId = customerPreferencesRequest.CustomerId,
            ProductId = customerPreferencesRequest.ProductId,
            Quantity = customerPreferencesRequest.Quantity,
            PreferredPurchaseDay = customerPreferencesRequest.PreferredPurchaseDay,
        };
    }
}