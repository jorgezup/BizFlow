using Application.DTOs.Sale;
using Application.UseCases.Sale.Create;
using Application.UseCases.Sale.Delete;
using Application.UseCases.Sale.GetAll;
using Application.UseCases.Sale.GetById;
using Application.UseCases.Sale.Update;
using Application.UseCases.Sale.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.Sale;

public static class ServiceCollectionExtensions
{
    public static void AddSaleUseCases(this IServiceCollection services)
    {
        // Add Validators
        services.AddScoped<IValidator<SaleRequest>, SaleValidator>();

        // Add Use Cases
        services.AddScoped<ICreateSaleUseCase, CreateSaleUseCase>();
        services.AddScoped<IDeleteSaleUseCase, DeleteSaleUseCase>();
        services.AddScoped<IGetAllSalesUseCase, GetAllSalesUseCase>();
        services.AddScoped<IGetSaleByIdUseCase, GetSaleByIdUseCase>();
        services.AddScoped<IUpdateSaleUseCase, UpdateSaleUseCase>();
    }
}