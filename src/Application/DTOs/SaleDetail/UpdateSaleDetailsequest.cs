namespace Application.DTOs.SaleDetail;

public record UpdateSaleDetailRequest(
    Guid? ProductId,
    decimal? Quantity
    );
