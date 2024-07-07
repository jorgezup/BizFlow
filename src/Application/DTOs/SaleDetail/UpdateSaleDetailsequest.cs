namespace Application.DTOs.SaleDetail;

public record UpdateSaleDetailRequest(
    Guid? SaleId,
    Guid? ProductId,
    decimal? Quantity,
    decimal? UnitPrice,
    decimal? Subtotal
    );
