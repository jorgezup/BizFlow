using Application.DTOs.Order;
using Application.Service.Order;
using Application.UseCases.Order.Create;
using Application.UseCases.Order.Delete;
using Application.UseCases.Order.GenerateOrders;
using Application.UseCases.Order.GetAll;
using Application.UseCases.Order.GetById;
using Application.UseCases.Order.Update;
using Application.UseCases.Order.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.Order;

public static class ServiceCollectionExtensions
{
    public static void AddOrderUseCases(this IServiceCollection services)
    {
        // Add UseCases
        services.AddScoped<CreateOrderUseCase>();
        services.AddScoped<GetOrderByIdUseCase>();
        services.AddScoped<GetAllPaginatedOrdersUseCase>();
        services.AddScoped<UpdateOrderStatusUseCase>();
        services.AddScoped<GenerateOrdersUseCase>();
        services.AddScoped<DeleteOrderUseCase>();

        // Add Validators
        services.AddScoped<IValidator<OrderRequest>, OrderValidator>();

        // Add Services
        services.AddScoped<IOrderService, OrderService>();
    }
}