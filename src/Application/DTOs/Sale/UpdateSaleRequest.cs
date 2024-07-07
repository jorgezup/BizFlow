namespace Application.DTOs.Sale;

public record UpdateSaleRequest(
    DateTime? SaleDate,
    decimal? TotalAmount,
    string? Status 
);
