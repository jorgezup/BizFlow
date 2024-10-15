using System.Text.Json;
using Application;
using Application.UseCases.Order.Update;
using Asp.Versioning;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

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
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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

                        // Configuração dos Health Checks
                        services.AddHealthChecks()
                            .AddSqlServer(
                                context.Configuration.GetConnectionString("DefaultConnection"),
                                name: "Banco de Dados SQL",
                                timeout: TimeSpan.FromSeconds(30),
                                tags: ["db", "sql", "sqlserver"]);

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

                        services.AddCors(options =>
                        {
                            options.AddPolicy(name: MyAllowSpecificOrigins,
                                policy =>
                                {
                                    policy.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                                });
                        });

                        services.AddMediatR(config =>
                        {
                            config.RegisterServicesFromAssembly(typeof(UpdateOrderStatusUseCase).Assembly);
                        });

                        services.AddControllers().AddNewtonsoftJson(options =>
                        {
                            options.SerializerSettings.Converters.Add(new StringEnumConverter());
                        });

                        services.AddInfrastructureServices();
                        services.AddApplicationServices();

                        services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "BizFlow API", Version = "v1" });
                        });
                    })
                    .Configure(app =>
                    {
                        app.UseCors(MyAllowSpecificOrigins);

                        app.UseRouting();

                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                            endpoints.MapHealthChecks("/health", new HealthCheckOptions
                            {
                                ResponseWriter = async (context, report) =>
                                {
                                    var result = JsonSerializer.Serialize(new
                                    {
                                        status = report.Status.ToString(),
                                        checks = report.Entries.Select(entry => new
                                        {
                                            name = entry.Key,
                                            status = entry.Value.Status.ToString(),
                                            exception = entry.Value.Exception?.Message,
                                            duration = entry.Value.Duration.ToString()
                                        })
                                    });
                                    context.Response.ContentType = "application/json";
                                    await context.Response.WriteAsync(result);
                                }
                            });
                        });

                        app.UseSwagger();
                        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BizFlow v1"));
                    });
            });
    }
}