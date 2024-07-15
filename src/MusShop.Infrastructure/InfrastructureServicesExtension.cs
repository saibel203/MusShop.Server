using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog.Events;
using Serilog;

namespace MusShop.Infrastructure;

public static class InfrastructureServicesExtension
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        const string outputTemplate =
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
        
        // Register And Setup Logger
        string logFilePath = GetLogFilePath(configuration);
        
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .MinimumLevel.Override("System", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .Enrich.WithEnvironmentName()
            .Enrich.FromLogContext()
            .Enrich.FromGlobalLogContext()
            .WriteTo.Console(
                restrictedToMinimumLevel: LogEventLevel.Debug,
                outputTemplate: outputTemplate)
            .WriteTo.File(
                path: logFilePath,
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: LogEventLevel.Information,
                rollOnFileSizeLimit: true,
                outputTemplate: outputTemplate)
            .CreateLogger();

        services.AddSerilog();
        
        return services;
    }

    private static string GetLogFilePath(IConfiguration configuration)
    {
        const string currentProjectName = "ApplicationNames:Infrastructure";
        const string logsPath = "Paths:LogPath";

        string workingDirectory = AppDomain.CurrentDomain.BaseDirectory;
        string? projectDirectory = Directory.GetParent(workingDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName;

        if (projectDirectory is null)
        {
            throw new DirectoryNotFoundException();
        }

        string logFilePath = Path.Combine(projectDirectory, configuration[currentProjectName] ?? string.Empty,
            configuration[logsPath] ?? string.Empty);

        return logFilePath;
    }
}