using Hangfire;
using Hangfire.Dashboard;
using HealthChecks.UI.Client;
using HealthChecks.UI.Configuration;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MusShop.Api;
using MusShop.Application;
using MusShop.Application.UseCases;
using MusShop.Infrastructure;
using MusShop.Infrastructure.Database.Seeds;
using MusShop.Jobs;
using MusShop.Persistence;
using MusShop.Persistence.Seeds;
using MusShop.Presentation.Middlewares;
using MusShop.Presentation.Middlewares.Extensions;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddInfrastructureServices<ExceptionHandlerMiddleware>(configuration);
builder.Services.AddPersistenceServices(configuration);
builder.Services.AddApplicationDtoService();
builder.Services.AddApplicationServices();
builder.Services.AddBaseServices();
builder.Services.AddHealthChecksServices(configuration);

WebApplication app = builder.Build();
IWebHostEnvironment environment = app.Environment;

DashboardOptions hangfireDashboardOptions = new DashboardOptions
{
    DashboardTitle = "Scheduler",
    Authorization = new List<IDashboardAuthorizationFilter>()
};

HealthCheckOptions healthCheckOptions = new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
    AllowCachingResponses = true,
    ResultStatusCodes =
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status500InternalServerError,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    }
};

if (environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.DisplayRequestDuration(); });

    using IServiceScope scope = app.Services.CreateScope();

    InitializeInfrastructureDbContext initInfrastructureContextInitialize =
        scope.ServiceProvider.GetRequiredService<InitializeInfrastructureDbContext>();
    await initInfrastructureContextInitialize.InitializeDatabaseAsync();

    InitializeDataDbContext initDataDbContext =
        scope.ServiceProvider.GetRequiredService<InitializeDataDbContext>();
    await initDataDbContext.InitializeDatabaseAsync();
}
else
{
    app.UseHsts();
}

app.UseSerilogRequestLogging();
app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.MapHealthChecks("/health", healthCheckOptions);
app.UseHealthChecksUI(GetUiHealthCheckOptions);

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/hangfire", hangfireDashboardOptions);

RecurringJob.AddOrUpdate<RestoreLogsDataJob>("restoreLogsDataJob",
    job => job.RestoreLogsData(), Cron.Monthly);

app.UseCors();

app.UseRouting();
app.MapControllers();

app.Run();

static void GetUiHealthCheckOptions(Options options)
{
    options.UIPath = "/health-ui";
    options.ApiPath = "/health-ui-api";
}