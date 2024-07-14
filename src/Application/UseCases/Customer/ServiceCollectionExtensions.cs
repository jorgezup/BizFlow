using Application.DTOs.Customer;
using Application.UseCases.Customer.Create;
using Application.UseCases.Customer.Delete;
using Application.UseCases.Customer.GetAll;
using Application.UseCases.Customer.GetById;
using Application.UseCases.Customer.Update;
using Application.UseCases.Customer.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.Customer;

public static class ServiceCollectionExtensions
{
    public static void AddCustomerUseCases(this IServiceCollection services)
    {
        // Add Validators
        services.AddScoped<IValidator<CustomerRequest>, CustomerValidator>();
        services.AddScoped<IValidator<CustomerUpdateRequest>, CustomerUpdateValidator>();

        // Add UseCases
        services.AddScoped<GetAllCustomersUseCase>();
        services.AddScoped<GetCustomerByIdUseCase>();
        services.AddScoped<UpdateCustomerUseCase>();
        services.AddScoped<CreateCustomerUseCase>();
        services.AddScoped<DeleteCustomerUseCase>();
    }
}