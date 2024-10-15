namespace Core.DTOs;

public class CustomerPendingPayment
{
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }
    public decimal TotalPendingAmount { get; set; }
}