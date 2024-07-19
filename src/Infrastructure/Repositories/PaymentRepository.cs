using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository(AppDbContext appDbContext) : IPaymentRepository
{
    public async Task<Payment?> GetPaymentByIdAsync(Guid paymentId)
    {
        return await appDbContext.Payments.FindAsync(paymentId);
    }

    public async Task<IEnumerable<Payment>> GetPaymentsBySaleIdAsync(Guid saleId)
    {
        return await appDbContext.Payments.Where(p => p.SaleId == saleId).ToListAsync();
    }

    public async Task<IEnumerable<Payment>> GetPaymentsBySaleIdsAsync(IEnumerable<Guid> saleIds)
    {
        return await appDbContext.Payments
            .Where(p => saleIds.Contains(p.SaleId))
            .ToListAsync();
    }

    public async Task AddPaymentAsync(Payment payment)
    {
        await appDbContext.Payments.AddAsync(payment);
    }

    public Task UpdatePaymentAsync(Payment payment)
    {
        appDbContext.Payments.Update(payment);
        return Task.CompletedTask;
    }
}