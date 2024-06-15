using Application.Interfaces;
using Application.Services;
using Application.Validators;
using Application.Validators.Customer;
using Core.Entities;
using Core.Models.Customer;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Add application services
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ISaleService, SaleService>();
        services.AddScoped<ISaleItemService, SaleItemService>();
        
        // Add validators
        services.AddScoped<IValidator<CustomerRequest>, CustomerValidator>();
        services.AddScoped<IValidator<CustomerUpdateRequest>, CustomerUpdateValidator>();
        services.AddScoped<IValidator<Product>, ProductValidator>();
    }
}