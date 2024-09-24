using Application.UseCases.Invoices.Generate;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.Invoices;

public static class ServiceCollectionExtensions
{
    public static void AddInvoiceUseCases(this IServiceCollection services)
    {
        services.AddScoped<GenerateInvoiceUseCase>();
    }
}