using Application.DTOs.CustomerPreferences;
using Application.UseCases.CustomerPreferences.Create;
using Application.UseCases.CustomerPreferences.Delete;
using Application.UseCases.CustomerPreferences.GetAll;
using Application.UseCases.CustomerPreferences.GetById;
using Application.UseCases.CustomerPreferences.Update;
using Application.UseCases.CustomerPreferences.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.CustomerPreferences;

public static class ServiceCollectionExtensions
{
    public static void AddCustomerPreferencesUseCases(this IServiceCollection services)
    {
        // Add Validators
        services.AddScoped<IValidator<CustomerPreferencesRequest>, CustomerPreferencesValidator>();
        services.AddScoped<IValidator<UpdateCustomerPreferencesRequest>, UpdateCustomerPreferencesValidator>();
        
        // Add UseCases
        services.AddScoped<CreateCustomerPreferences>();
        services.AddScoped<GetAllCustomerPreferences>();
        services.AddScoped<GetCustomerPreferencesById>();
        services.AddScoped<UpdateCustomerPreferences>();
        services.AddScoped<DeleteCustomerPreferences>();
    }
}