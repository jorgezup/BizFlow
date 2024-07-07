using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence;

public class UnitOfWork(AppDbContext context, IDbContextTransaction transaction) : IUnitOfWork
{
    public IProductRepository ProductRepository => new ProductRepository(context);
    public ICustomerRepository CustomerRepository => new CustomerRepository(context);
    public IPriceHistoryRepository PriceHistoryRepository => new PriceHistoryRepository(context);
    public ICustomerPreferencesRepository CustomerPreferencesRepository => new CustomerPreferencesRepository(context);
    public ISaleDetailRepository SaleDetailRepository => new SaleDetailRepository(context);
    public ISaleRepository SaleRepository => new SaleRepository(context);

    public async Task BeginTransactionAsync()
    {
        transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await transaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await transaction.RollbackAsync();
    }

    public void Dispose()
    {
        transaction.Dispose();
        context.Dispose();
    }
}