using Core.Entities;

namespace Core.Interfaces;

public interface IPaymentRepository
{
    Task<Payment?> GetPaymentByIdAsync(Guid paymentId);
    Task<IEnumerable<Payment>> GetPaymentsBySaleIdAsync(Guid saleId);
    Task<IEnumerable<Payment>> GetPaymentsBySaleIdsAsync(IEnumerable<Guid> saleIds);
    Task AddPaymentAsync(Payment payment);
    Task UpdatePaymentAsync(Payment payment);
    Task DeletePaymentAsync(Guid paymentId);
}