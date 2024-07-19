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
    ISaleRepository saleRepository,
    ISaleDetailRepository saleDetailRepository,
    IPaymentRepository paymentRepository)
    : IUnitOfWork
{
    public IProductRepository ProductRepository { get; } = productRepository;
    public ICustomerRepository CustomerRepository { get; } = customerRepository;
    public IPriceHistoryRepository PriceHistoryRepository { get; } = priceHistoryRepository;
    public ICustomerPreferencesRepository CustomerPreferencesRepository { get; } = customerPreferencesRepository;
    public ISaleDetailRepository SaleDetailRepository { get; } = saleDetailRepository;
    public ISaleRepository SaleRepository { get; } = saleRepository;

    public IPaymentRepository PaymentRepository { get; } = paymentRepository;

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