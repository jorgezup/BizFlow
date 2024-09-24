using Application.UseCases.Customer;
using Application.UseCases.CustomerPreferences;
using Application.UseCases.Invoices;
using Application.UseCases.Order;
using Application.UseCases.OrderDetail;
using Application.UseCases.Payment;
using Application.UseCases.PriceHistory;
using Application.UseCases.Product;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases;

public static class ServiceCollectionExtensions
{
    public static void AddUseCases(this IServiceCollection services)
    {
        // Customer
        services.AddCustomerUseCases();

        // Product
        services.AddProductUseCases();

        // PriceHistory
        services.AddPriceHistoryUseCases();

        // CustomerPreferences
        services.AddCustomerPreferencesUseCases();

        // OrderDetail
        services.AddSaleDetailUseCases();

        // Payment
        services.AddPaymentUseCases();

        // Order
        services.AddOrderUseCases();
        
        // Invoice
        services.AddInvoiceUseCases();
    }
}