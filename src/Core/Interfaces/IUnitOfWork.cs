namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    ISaleDetailRepository OrderDetailRepository { get; }
    IProductRepository ProductRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    ICustomerPreferencesRepository CustomerPreferencesRepository { get; }
    IPriceHistoryRepository PriceHistoryRepository { get; }
    IPaymentRepository PaymentRepository { get; }
    IOrderRepository OrderRepository { get; }
    IOrderLifeCycleRepository OrderLifeCycleRepository { get; }
}