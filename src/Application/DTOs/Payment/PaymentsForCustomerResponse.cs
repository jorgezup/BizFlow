namespace Application.DTOs.Payment;

public class PaymentsForCustomerResponse
{
    public decimal TotalPurchases { get; set; }
    public decimal TotalPaid { get; set; }
    public decimal TotalRemaining { get; set; }
    public List<SaleDetailResponse> SalesCreated { get; set; }
    public List<SaleDetailResponse> SalesDelivered { get; set; }
    public List<SaleDetailResponse> SalesCancelled { get; set; }
}

public class SaleDetailResponse
{
    public Guid SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public List<string> Products { get; set; }
    public decimal TotalAmount { get; set; }
}