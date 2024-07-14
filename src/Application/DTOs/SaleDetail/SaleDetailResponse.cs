namespace Application.DTOs.SaleDetail;

public record SaleDetailResponse(
    Guid Id,
    Guid SaleId,
    string ProductName,
    decimal Quantity,
    decimal UnitPrice,
    decimal Subtotal,
    DateTime CreatedAt,
    DateTime UpdatedAt);