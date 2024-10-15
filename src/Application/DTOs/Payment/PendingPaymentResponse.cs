using Core.DTOs;
using OrderResponse = Core.DTOs.OrderResponse;

namespace Application.DTOs.Payment;

public class PendingPaymentResponse
{
    public List<OrderResponse> Orders { get; set; }
    public List<CustomerPendingPayment> CustomerPendingPayment { get; set; }
    public decimal TotalPendingAmount { get; set; }
}
