using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PaymentRepository(AppDbContext appDbContext) : IPaymentRepository
{
    public async Task<PaymentResponse> GetPaymentByIdAsync(Guid paymentId)
    {
        return await appDbContext.Payments
            .Where(p => p.Id == paymentId)
            .Select(payment => new PaymentResponse(
                payment.Id,
                payment.OrderId,
                payment.Order.CustomerId,
                payment.Order.Customer.Name,
                payment.Amount,
                payment.PaymentDate,
                payment.PaymentMethod.ToString(),
                payment.CreatedAt,
                payment.UpdatedAt
            ))
            .FirstOrDefaultAsync();
    }

    public Task<bool> ExistsPaymentByOrderIdAsync(Guid orderId)
    {
        return appDbContext.Payments
            .AnyAsync(p => p.OrderId == orderId);
    }

    public async Task<IEnumerable<PaymentResponse>> GetAllPaymentsWithFiltersAsync(
        int page,
        int pageSize,
        Guid? customerId,
        DateTime? startDate,
        DateTime? endDate,
        string? sortColumn,
        string? sortDirection)
    {
        var query = appDbContext.Payments.AsQueryable();

        // Aplicar filtros
        if (!string.IsNullOrEmpty(customerId.ToString()))
        {
            query = query.Where(p => p.Order.CustomerId == customerId);
        }

        if (startDate.HasValue)
        {
            query = query.Where(p => p.PaymentDate >= startDate.Value.Date);
        }

        if (endDate.HasValue)
        {
            query = query.Where(p => p.PaymentDate <= endDate.Value.Date.AddDays(1));
        }

        // Aplicar ordenação
        if (!string.IsNullOrEmpty(sortColumn))
        {
            query = sortColumn switch
            {
                "CustomerName" =>
                    sortDirection?.ToLower() == "desc"
                        ? query.OrderByDescending(p => p.Order.Customer.Name)
                        : query.OrderBy(p => p.Order.Customer.Name),
                _ => sortDirection?.ToLower() == "desc"
                    ? query.OrderByDescending(p => EF.Property<object>(p, sortColumn))
                    : query.OrderBy(p => EF.Property<object>(p, sortColumn))
            };
        }

        // Paginação e projeção para PaymentResponse
        return await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(payment => new PaymentResponse(
                payment.Id,
                payment.Order.Id,
                payment.Order.Customer.Id,
                payment.Order.Customer.Name,
                payment.Amount,
                payment.PaymentDate,
                payment.PaymentMethod.ToString(), // Se for enum
                payment.CreatedAt,
                payment.UpdatedAt
            ))
            .ToListAsync();
    }


    public async Task<int> GetAllPaymentsWithFiltersCountAsync(Guid? customerId, DateTime? startDate,
        DateTime? endDate)
    {
        var query = appDbContext.Payments.AsQueryable();

        // Aplicar filtros
        if (!string.IsNullOrEmpty(customerId.ToString()))
        {
            query = query.Where(p => p.Order.CustomerId == customerId);
        }

        if (startDate.HasValue)
        {
            query = query.Where(p => p.PaymentDate >= startDate.Value.Date);
        }

        if (endDate.HasValue)
        {
            query = query.Where(p => p.PaymentDate <= endDate.Value.Date.AddDays(1));
        }

        // Retornar a contagem de registros
        return await query.CountAsync();
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

    public async Task DeletePaymentAsync(Guid paymentId)
    {
        var payment = await appDbContext.Payments.FindAsync(paymentId);
        if (payment is not null)
        {
            appDbContext.Payments.Remove(payment);
        }
    }
}