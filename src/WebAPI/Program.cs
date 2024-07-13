using System.Text.Json;
using Application;
using Asp.Versioning;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace WebAPI;

public abstract class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            DatabaseInitializer.Initialize(services);
        }

        host.Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((context, services) =>
                    {
                        services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));


                        services.AddControllers()
                            .AddJsonOptions(options =>
                            {
                                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                            });

                        services.AddApiVersioning(options =>
                        {
                            options.DefaultApiVersion = new ApiVersion(1, 0);
                            options.AssumeDefaultVersionWhenUnspecified = true;
                            options.ReportApiVersions = true;
                        }).AddApiExplorer(options =>
                        {
                            options.GroupNameFormat = "'v'VVV";
                            options.SubstituteApiVersionInUrl = true;
                        });

                        services.AddApplicationServices();
                        services.AddInfrastructureServices();

                        if (context.HostingEnvironment.IsDevelopment())
                            services.AddSwaggerGen(c =>
                            {
                                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BizFlow API", Version = "v1" });
                            });
                    })
                    .Configure(app =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

                        if (!app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment()) return;

                        app.UseSwagger();
                        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BizFlow v1"));
                    });
            });
    }
}