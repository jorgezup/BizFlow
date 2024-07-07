using Application.UseCases.SaleDetail.Create;
using Application.UseCases.SaleDetail.Delete;
using Application.UseCases.SaleDetail.GetAll;
using Application.UseCases.SaleDetail.GetById;
using Application.UseCases.SaleDetail.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.SaleDetail;

public static class ServiceCollectionExtensions
{
    public static void AddSaleDetailUseCases(this IServiceCollection services)
    {
        services.AddScoped<IGetAllSalesDetailsUseCase, GetAllSalesDetailsUseCase>();
        services.AddScoped<IGetSaleDetailByIdUseCase, GetSaleDetailByIdUseCase>();
        services.AddScoped<IUpdateSaleDetailUseCase, UpdateSaleDetailUseCase>();
        services.AddScoped<ICreateSaleDetailUseCase, CreateSaleDetailUseCase>();
        services.AddScoped<IDeleteSaleDetailUseCase, DeleteSaleDetailUseCase>();
    }
}