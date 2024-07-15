using MusShop.Api;
using MusShop.Infrastructure;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices();
builder.Services.AddBaseServices();

WebApplication app = builder.Build();

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();