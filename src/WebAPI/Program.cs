using System.Text.Json;
using Application;
using Asp.Versioning;
using Infrastructure;
using Infrastructure.Data;
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

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
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

                        services.AddApiVersioning(option =>
                        {
                            option.AssumeDefaultVersionWhenUnspecified = true; //This ensures if client doesn't specify an API version. The default version should be considered. 
                            option.DefaultApiVersion = new ApiVersion(1, 0); //This we set the default API version
                            // option.ReportApiVersions = true; //The allow the API Version information to be reported in the client  in the response header. This will be useful for the client to understand the version of the API they are interacting with.
    
                            //------------------------------------------------//
                            // option.ApiVersionReader = ApiVersionReader.Combine(
                            //     new QueryStringApiVersionReader("api-version"),
                            //     new HeaderApiVersionReader("X-Version"),
                            //     new MediaTypeApiVersionReader("ver")); //This says how the API version should be read from the client's request, 3 options are enabled 1.Querystring, 2.Header, 3.MediaType. 
                            //"api-version", "X-Version" and "ver" are parameter name to be set with version number in client before request the endpoints.
                        }).AddApiExplorer(options => {
                            options.GroupNameFormat = "'v'VVV"; //The say our format of our version number “‘v’major[.minor][-status]”
                            options.SubstituteApiVersionInUrl = true; //This will help us to resolve the ambiguity when there is a routing conflict due to routing template one or more end points are same.
                        });

                        services.AddApplicationServices();
                        services.AddInfrastructureServices();

                        if (context.HostingEnvironment.IsDevelopment())
                        {
                            services.AddSwaggerGen(c =>
                            {
                                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BizFlow API", Version = "v1" });
                            });
                        }
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