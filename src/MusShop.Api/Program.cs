using MusShop.Api;
using MusShop.Infrastructure;
using MusShop.Infrastructure.Database;
using MusShop.Presentation.Middlewares;
using MusShop.Presentation.Middlewares.Extensions;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddInfrastructureServices<ExceptionHandlerMiddleware>(configuration);
builder.Services.AddBaseServices();

WebApplication app = builder.Build();
IWebHostEnvironment environment = app.Environment;

if (environment.IsDevelopment())
{
    using IServiceScope scope = app.Services.CreateScope();

    SeedInfrastructureDbContext initInfrastructureContextSeed =
        scope.ServiceProvider.GetRequiredService<SeedInfrastructureDbContext>();
    await initInfrastructureContextSeed.InitializeDatabaseAsync();
}

app.UseSerilogRequestLogging();
app.UseExceptionMiddleware();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();