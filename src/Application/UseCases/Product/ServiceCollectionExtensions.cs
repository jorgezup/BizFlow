using Application.UseCases.Product.Create;
using Application.UseCases.Product.Delete;
using Application.UseCases.Product.GetAll;
using Application.UseCases.Product.GetById;
using Application.UseCases.Product.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.Product;

public static class ServiceCollectionExtensions
{
    public static void AddProductUseCases(this IServiceCollection services)
    {
        services.AddScoped<CreateProduct>();
        services.AddScoped<GetAllProducts>();
        services.AddScoped<GetProductById>();
        services.AddScoped<UpdateProduct>();
        services.AddScoped<DeleteProduct>();
    }
}