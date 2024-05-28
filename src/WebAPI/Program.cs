// using Application.Interfaces;
// using Application.Services;
// using Core.Interfaces;
// using Infrastructure;
// using Infrastructure.Data;
// using Infrastructure.Repositories;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.OpenApi.Models;
//
// var builder = WebApplication.CreateBuilder(args);
//
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo { Title = "Sales API", Version = "v1" });
// });
//
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//
// builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
// builder.Services.AddScoped<ICustomerService, CustomerService>();
//
// builder.Services.AddControllers();
//
// var app = builder.Build();
//
// // Apply migrations
// DatabaseInitializer.Initialize(app.Services);
//
// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
//     app.UseSwagger();
//     app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BizFlow v1"));
// }
//
// app.UseRouting();
//
// app.UseEndpoints(endpoints =>
// {
//     endpoints.MapControllers();
// });
//
// app.Run();

using Application;
using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace WebAPI;

public abstract class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((context, services) =>
                    {
                        services.AddDbContext<AppDbContext>(options =>
                            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                        services.AddControllers();
                        
                        // services.AddScoped<ICustomerRepository, CustomerRepository>();
                        // services.AddScoped<ICustomerService, CustomerService>();

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
                    .Configure((app) =>
                    {
                        app.UseRouting();
                        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

                        if (!app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment()) return;
                        
                        app.UseSwagger();
                        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BizFlow v1"));
                    });
            });
}