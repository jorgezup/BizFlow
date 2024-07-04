namespace Application.DTOs.CustomerPreferences;

public static class CustomerPreferencesExtensions
{
    public static CustomerPreferencesResponse MapToCustomerPreferencesResponse(
        this Core.Entities.CustomerPreferences customerPreferences)
    {
        return new CustomerPreferencesResponse(
            customerPreferences.Id,
            customerPreferences.CustomerId,
            customerPreferences.ProductId,
            customerPreferences.PreferredPurchaseDays,
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
            PreferredPurchaseDays = customerPreferencesRequest.PreferredPurchaseDays,
        };
    }
}