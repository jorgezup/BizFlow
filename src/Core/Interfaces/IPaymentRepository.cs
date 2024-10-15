using Core.DTOs;
using Core.Entities;

namespace Core.Interfaces;

public interface IPaymentRepository
{
    Task<PaymentResponse> GetPaymentByIdAsync(Guid paymentId);
    Task<bool> ExistsPaymentByOrderIdAsync(Guid orderId);
    Task<IEnumerable<PaymentResponse>> GetAllPaymentsAsync(
        Guid? customerId,
        DateTime? startDate,
        DateTime? endDate,
        string? sortColumn,
        string? sortDirection);
    Task<IEnumerable<PaymentResponse>> GetAllPaginatedPaymentsAsync(
        int page,
        int pageSize,
        Guid? customerId,
        DateTime? startDate,
        DateTime? endDate,
        string? sortColumn,
        string? sortDirection);
    Task<int> GetTotalPaginatedPaymentsAsync(
        Guid? customerId,
        DateTime? startDate,
        DateTime? endDate);
    Task AddPaymentAsync(Payment payment);
    Task UpdatePaymentAsync(Payment payment);
    Task DeletePaymentAsync(Guid paymentId);
}