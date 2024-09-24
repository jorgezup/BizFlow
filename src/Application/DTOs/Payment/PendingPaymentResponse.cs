using Core.DTOs;
using OrderResponse = Core.DTOs.OrderResponse;

namespace Application.DTOs.Payment;

public class PendingPaymentResponse
{
    public IEnumerable<OrderResponse> Orders { get; set; }
    public IEnumerable<CustomerPendingPayment> CustomerPendingPayment { get; set; }
    public decimal TotalPendingAmount { get; set; }
}