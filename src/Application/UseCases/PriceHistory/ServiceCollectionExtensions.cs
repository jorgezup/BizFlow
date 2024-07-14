using Application.UseCases.PriceHistory.Create;
using Application.UseCases.PriceHistory.Delete;
using Application.UseCases.PriceHistory.GetById;
using Application.UseCases.PriceHistory.Update;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UseCases.PriceHistory;

public static class ServiceCollectionExtensions
{
    public static void AddPriceHistoryUseCases(this IServiceCollection services)
    {
        // Add UseCases
        // services.AddScoped<IGetAllPriceHistoriesUseCase, GetAllPriceHistoriesUseCase>();
        services.AddScoped<GetPriceHistoryByIdUseCase>();
        services.AddScoped<UpdatePriceHistoryUseCase>();
        services.AddScoped<CreatePriceHistoryUseCase>();
        services.AddScoped<DeletePriceHistoryUseCase>();
    }
}