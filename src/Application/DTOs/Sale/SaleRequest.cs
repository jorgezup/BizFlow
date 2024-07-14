using Application.DTOs.SaleDetail;

namespace Application.DTOs.Sale;

public record SaleRequest(
    Guid CustomerId,
    DateTime SaleDate,
    List<SaleDetailRequestToSale> SaleDetails);
