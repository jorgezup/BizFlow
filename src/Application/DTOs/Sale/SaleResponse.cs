namespace Application.DTOs.Sale;

public record SaleResponse(
    Guid Id,
    Guid CustomerId,
    DateTime SaleDate,
    decimal TotalAmount,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt);
