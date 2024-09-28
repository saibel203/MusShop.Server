using System.Reflection;
using HealthChecks.Hangfire;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using MusShop.Api.HealthChecks;
using MusShop.Domain.Services.Helpers;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MusShop.Api;

public static class BaseServicesExtension
{
    public static IServiceCollection AddBaseServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

        // Swagger settings
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(GetSwaggerGenOptions);

        return services;
    }
    
    public static IServiceCollection AddHealthChecksServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        const string databaseHealthQuery = "SELECT 1";
        
        string infrastructureDbConnectionString =
            ConnectionStringHelper.GetInfrastructureDbConnectionString(configuration);
        string dataDbConnectionString =
            ConnectionStringHelper.GetDataDbConnectionString(configuration);
        
        // HealthChecks
        services.AddHealthChecks()
            .AddCheck<MemoryHealthCheck>(
                name: "Feedback Service Memory Check",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "Feedback Service" })
            .AddSqlServer(
                connectionString: infrastructureDbConnectionString,
                healthQuery: databaseHealthQuery,
                name: "Infrastructure SQL server",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "Database" })
            .AddSqlServer(
                connectionString: dataDbConnectionString,
                healthQuery: databaseHealthQuery,
                name: "Data SQL server",
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "Database" })
            .AddHangfire(
                GetHangfireOptions, 
                name: "Hangfire check", 
                failureStatus: HealthStatus.Unhealthy,
                tags: new[] { "Background" });
        
        services.AddHealthChecksUI()
            .AddSqlServerStorage(infrastructureDbConnectionString);

        return services;
    }

    private static void GetSwaggerGenOptions(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "MusShop API",
            Description = "This API is intended for the MusShop music store"
        });

        string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        string presentationXmlFile = "MusShop.Presentation.xml";
        string applicationDtoXmlFile = "MusShop.Application.Dto.xml";

        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, presentationXmlFile));
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, applicationDtoXmlFile));
    }

    private static void GetHangfireOptions(HangfireOptions options)
    {
        options.MaximumJobsFailed = 1;
        options.MinimumAvailableServers = 1;
    }
}