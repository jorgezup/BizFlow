using Core.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPriceHistoryRepository, PriceHistoryRepository>();
        services.AddScoped<ICustomerPreferencesRepository, CustomerPreferencesRepository>();
        services.AddScoped<ISaleDetailRepository, SaleDetailRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderLifeCycleRepository, OrderLifeCycleRepository>();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}