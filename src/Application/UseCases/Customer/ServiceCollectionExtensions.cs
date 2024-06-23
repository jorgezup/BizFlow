using Application.UseCases.Customer.Create;
using Application.UseCases.Customer.Delete;
using Application.UseCases.Customer.GetAll;
using Application.UseCases.Customer.GetById;
using Application.UseCases.Customer.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.Customer;

public static class ServiceCollectionExtensions
{
    public static void AddCustomerUseCases(this IServiceCollection services)
    {
        services.AddScoped<CreateCustomer>();
        services.AddScoped<GetAllCustomers>();
        services.AddScoped<GetCustomerById>();
        services.AddScoped<UpdateCustomer>();
        services.AddScoped<DeleteCustomer>();
    }
}