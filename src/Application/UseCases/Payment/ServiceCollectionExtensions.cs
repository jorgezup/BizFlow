using Application.UseCases.Payment.Create;
using Application.UseCases.Payment.Delete;
using Application.UseCases.Payment.GetAll;
using Application.UseCases.Payment.GetById;
using Application.UseCases.Payment.GetPendingPayments;
using Application.UseCases.Payment.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.Payment;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPaymentUseCases(this IServiceCollection services)
    {
        //Add UseCases
        services.AddScoped<CreatePaymentUseCase>();
        services.AddScoped<GetPaymentByIdUseCase>();
        services.AddScoped<GetAllPaymentsUseCase>();
        services.AddScoped<UpdatePaymentUseCase>();
        services.AddScoped<DeletePaymentUseCase>();
        services.AddScoped<GetPendingPaymentsUseCase>();

        return services;
    }
}