namespace Application.DTOs.Sale;

public static class SaleExtensions
{
    public static SaleResponse MapToSaleResponse(this Core.Entities.Sale sale)
    {
        return new SaleResponse(
            sale.Id,
            sale.CustomerId,
            sale.SaleDate,
            sale.TotalAmount,
            sale.Status,
            sale.CreatedAt,
            sale.UpdatedAt);
    }
    
    public static Core.Entities.Sale MapToSale(this SaleRequest saleRequest)
    {
        return new Core.Entities.Sale
        {
            Id = Guid.NewGuid(),
            CustomerId = saleRequest.CustomerId,
            SaleDate = saleRequest.SaleDate,
            TotalAmount = 0,
            Status = "PENDING",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
    
    public static Core.Entities.Sale MapToSale(this Core.Entities.Sale sale, UpdateSaleRequest updateSaleRequest)
    {
        return new Core.Entities.Sale
        {
            Id = sale.Id,
            CustomerId = sale.CustomerId,
            SaleDate = updateSaleRequest.SaleDate ?? sale.SaleDate,
            TotalAmount = updateSaleRequest.TotalAmount ?? sale.TotalAmount,
            Status = updateSaleRequest.Status ?? sale.Status,
            UpdatedAt = DateTime.UtcNow
        };
    }
}
