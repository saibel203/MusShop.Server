using MusShop.Api;
using MusShop.Infrastructure;
using MusShop.Presentation.Middlewares;
using MusShop.Presentation.Middlewares.Extensions;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddInfrastructureServices<ExceptionHandlerMiddleware>(configuration);
builder.Services.AddBaseServices();

WebApplication app = builder.Build();

app.UseSerilogRequestLogging();
app.UseExceptionMiddleware();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();