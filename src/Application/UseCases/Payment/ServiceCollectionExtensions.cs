using Application.UseCases.Payment.Create;
using Application.UseCases.Payment.GetById;
using Application.UseCases.Payment.GetBySaleId;
using Application.UseCases.Payment.GetTotalPaymentsForCustomer;
using Application.UseCases.Payment.GetRemainingBalanceForCustomer;
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
        services.AddScoped<GetPaymentsBySaleIdUseCaseUseCase>();
        services.AddScoped<GetRemainingBalanceForCustomerUseCase>();
        services.AddScoped<GetTotalPaymentsForCustomerUseCase>();
        services.AddScoped<UpdatePaymentUseCase>();

        return services;
    }
}