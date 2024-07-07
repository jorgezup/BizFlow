using Application.DTOs.Product;
using Application.UseCases.Product.Create;
using Application.UseCases.Product.Delete;
using Application.UseCases.Product.GetAll;
using Application.UseCases.Product.GetById;
using Application.UseCases.Product.Update;
using Application.UseCases.Product.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.Product;

public static class ServiceCollectionExtensions
{
    public static void AddProductUseCases(this IServiceCollection services)
    {
        // Add Validators
        services.AddScoped<IValidator<ProductRequest>, ProductValidator>();
        // services.AddScoped<IValidator<ProductUpdateRequest>, ProductUpdateValidator>();

        // Add UseCases
        services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
        services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
        services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
        services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
    }
}