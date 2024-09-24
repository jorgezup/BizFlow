using Application.DTOs.OrderDetail;
using Application.Service.OrderDetail;
using Application.UseCases.OrderDetail.Create;
using Application.UseCases.OrderDetail.Delete;
using Application.UseCases.OrderDetail.GetAll;
using Application.UseCases.OrderDetail.GetById;
using Application.UseCases.OrderDetail.GetBySaleId;
using Application.UseCases.OrderDetail.Update;
using Application.UseCases.OrderDetail.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.OrderDetail;

public static class ServiceCollectionExtensions
{
    public static void AddSaleDetailUseCases(this IServiceCollection services)
    {
        // Add Validators
        services.AddScoped<IValidator<OrderDetailRequest>, OrderDetailValidator>();

        // Add Use Cases
        services.AddScoped<GetAllOrdersDetailsUseCase>();
        services.AddScoped<GetOrderDetailByIdUseCase>();
        services.AddScoped<GetOrderDetailByOrderIdUseCase>();
        services.AddScoped<CreateOrderDetailUseCase>();
        services.AddScoped<UpdateOrderDetailUseCase>();
        services.AddScoped<UpdateOrderDetailByOrderIdUseCase>();
        services.AddScoped<DeleteOrderDetailUseCase>();
        
        // Add Services
        services.AddScoped<IOrderDetailService, OrderDetailService>();
    }
}