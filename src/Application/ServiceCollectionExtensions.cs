using Application.DTOs.Customer;
using Application.DTOs.Product;
using Application.UseCases;
using Application.Validators.Customer;
using Application.Validators.Product;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Add validators
        services.AddScoped<IValidator<CustomerRequest>, CustomerValidator>();
        services.AddScoped<IValidator<CustomerUpdateRequest>, CustomerUpdateValidator>();

        services.AddScoped<IValidator<ProductRequest>, ProductValidator>();
        services.AddScoped<IValidator<ProductUpdateRequest>, ProductUpdateValidator>();

        // Add UseCases
        services.AddUseCases();
    }
}