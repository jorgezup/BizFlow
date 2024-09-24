using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence;

public class UnitOfWork(
    AppDbContext context,
    IProductRepository productRepository,
    ICustomerRepository customerRepository,
    IPriceHistoryRepository priceHistoryRepository,
    ICustomerPreferencesRepository customerPreferencesRepository,
    ISaleDetailRepository saleDetailRepository,
    IPaymentRepository paymentRepository,
    IOrderRepository orderRepository,
    IOrderLifeCycleRepository orderLifeCycleRepository)
    : IUnitOfWork
{
    public IProductRepository ProductRepository { get; } = productRepository;
    public ICustomerRepository CustomerRepository { get; } = customerRepository;
    public IPriceHistoryRepository PriceHistoryRepository { get; } = priceHistoryRepository;
    public ICustomerPreferencesRepository CustomerPreferencesRepository { get; } = customerPreferencesRepository;
    public ISaleDetailRepository OrderDetailRepository { get; } = saleDetailRepository;
    public IPaymentRepository PaymentRepository { get; } = paymentRepository;
    public IOrderRepository OrderRepository { get; } = orderRepository;
    public IOrderLifeCycleRepository OrderLifeCycleRepository { get; } = orderLifeCycleRepository;

    private IDbContextTransaction? _transaction;

    public async Task BeginTransactionAsync()
    {
        _transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        try
        {
            await context.SaveChangesAsync();
            await _transaction?.CommitAsync()!;
        }
        catch
        {
            await _transaction?.RollbackAsync()!;
            throw;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        await _transaction?.RollbackAsync()!;
    }

    public void Dispose()
    {
        _transaction?.Dispose();
        context.Dispose();
    }
}