using Application.DTOs.SaleDetail;
using Application.UseCases.SaleDetail.Create;
using Application.UseCases.SaleDetail.Delete;
using Application.UseCases.SaleDetail.GetAll;
using Application.UseCases.SaleDetail.GetById;
using Application.UseCases.SaleDetail.GetBySaleId;
using Application.UseCases.SaleDetail.Update;
using Application.UseCases.SaleDetail.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.SaleDetail;

public static class ServiceCollectionExtensions
{
    public static void AddSaleDetailUseCases(this IServiceCollection services)
    {
        // Add Validators
        services.AddScoped<IValidator<SaleDetailRequest>, SaleDetailValidator>();

        // Add Use Cases
        services.AddScoped<GetAllSalesDetailsUseCase>();
        services.AddScoped<GetSaleDetailByIdUseCase>();
        services.AddScoped<GetSaleDetailBySaleIdUseCase>();
        services.AddScoped<UpdateSaleDetailUseCase>();
        services.AddScoped<CreateSaleDetailUseCase>();
        services.AddScoped<DeleteSaleDetailUseCase>();
    }
}