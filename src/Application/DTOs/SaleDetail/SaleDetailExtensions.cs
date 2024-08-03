namespace Application.DTOs.SaleDetail;

public static class SaleDetailExtensions
{
    public static SaleDetailResponse MapToSaleDetailResponse(this Core.Entities.SaleDetail saleDetail)
    {
        return new SaleDetailResponse(
            saleDetail.Id,
            saleDetail.SaleId,
            saleDetail.Product.Name,
            saleDetail.Quantity,
            saleDetail.UnitPrice,
            saleDetail.Subtotal,
            saleDetail.CreatedAt,
            saleDetail.UpdatedAt);
    }

    public static Core.Entities.SaleDetail MapToSaleDetail(this SaleDetailRequest saleDetailRequest)
    {
        return new Core.Entities.SaleDetail
        {
            Id = Guid.NewGuid(),
            SaleId = saleDetailRequest.SaleId,
            ProductId = saleDetailRequest.ProductId,
            Quantity = saleDetailRequest.Quantity,
            UnitPrice = saleDetailRequest.UnitPrice,
            Subtotal = saleDetailRequest.Subtotal,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }
    
    public static Core.Entities.SaleDetail UpdateSaleDetail(this Core.Entities.SaleDetail saleDetail,
        UpdateSaleDetailRequest saleDetailUpdateRequest)
    {
        saleDetail.ProductId = saleDetailUpdateRequest.ProductId ?? saleDetail.ProductId;
        saleDetail.Quantity = (decimal)(saleDetailUpdateRequest.Quantity > 0 ? saleDetailUpdateRequest.Quantity  : saleDetail.Quantity);
        saleDetail.UpdatedAt = DateTime.UtcNow;
        return saleDetail;
    }
}