namespace Core.DTOs;

public record CustomerPendingPayment(Guid CustomerId, string CustomerName, decimal TotalPendingAmount);