namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    ISaleDetailRepository SaleDetailRepository { get; }
    IProductRepository ProductRepository { get; }
    ISaleRepository SaleRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    ICustomerPreferencesRepository CustomerPreferencesRepository { get; }
    IPriceHistoryRepository PriceHistoryRepository { get; }
    IPaymentRepository PaymentRepository { get; }
}