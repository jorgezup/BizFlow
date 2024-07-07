namespace Application.DTOs.SaleDetail;

public record SaleDetailResponse(
    Guid Id,
    Guid SaleId,
    Guid ProductId,
    decimal Quantity,
    decimal UnitPrice,
    decimal Subtotal,
    DateTime CreatedAt,
    DateTime UpdatedAt);