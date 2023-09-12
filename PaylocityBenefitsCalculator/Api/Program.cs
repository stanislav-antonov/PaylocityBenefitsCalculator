using Api;
using Api.Repositories;
using Api.Repository;
using Api.Services;
using AutoMapper;
using Microsoft.OpenApi.Models;

internal class Program
{
    private const string allowLocalhost = "allow localhost";

    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        AddSwagger(builder);
        AddCORS(builder);
        AddMapper(builder);
        AddLogging(builder);

        AddRepositories(builder);
        AddDomainServices(builder);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(allowLocalhost);

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static void AddSwagger(WebApplicationBuilder builder)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.EnableAnnotations();
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Employee Benefit Cost Calculation Api",
                Description = "Api to support employee benefit cost calculations"
            });
        });
    }

    private static void AddMapper(WebApplicationBuilder builder)
    {
        var mapperConfiguration = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MapperProfile());
        });

        builder.Services.AddSingleton(mapperConfiguration.CreateMapper());
    }

    private static void AddCORS(WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(allowLocalhost,
                policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
        });
    }

    private static void AddRepositories(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IEmployeesRepository, EmployeesRepository>();
        builder.Services.AddSingleton<IDependentsRepository, DependentsRepository>();
        builder.Services.AddSingleton<IPaycheckCalcParamsRepository, PaycheckCalcParamsRepository>();
    }

    private static void AddDomainServices(WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IPaycheckService, PaycheckService>();
    }

    private static void AddLogging(WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();
    }
}