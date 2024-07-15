using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SendGrid;
using Serilog.Events;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.Email;

namespace MusShop.Infrastructure;

public static class InfrastructureServicesExtension
{
    public static IServiceCollection AddInfrastructureServices<TExceptionMiddleware>(
        this IServiceCollection services, IConfiguration configuration)
        where TExceptionMiddleware : class
    {
        const string outputTemplate =
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

        // Register And Setup Logger
        string logFilePath = GetLogFilePath(configuration);
        EmailConnectionInfo emailConnectionInfo = GetEmailConnectionInfoData(configuration);

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
            .WriteTo.Logger(logger => logger
                .Filter.ByIncludingOnly(Matching.FromSource<TExceptionMiddleware>())
                .WriteTo.Email(
                    connectionInfo: emailConnectionInfo,
                    restrictedToMinimumLevel: LogEventLevel.Error))
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

    private static EmailConnectionInfo GetEmailConnectionInfoData(IConfiguration configuration)
    {
        const string fromEmailConfiguration = "SmtpErrorsConfiguration:FromEmail";
        const string toEmailConfiguration = "SmtpErrorsConfiguration:ToEmail";
        const string subjectEmailConfiguration = "SmtpErrorsConfiguration:ToEmail";
        const string fromNameEmailConfiguration = "SmtpErrorsConfiguration:FromName";
        const string portEmailConfiguration = "SmtpErrorsConfiguration:Port";
        const string sslEmailConfiguration = "SmtpErrorsConfiguration:EnableSsl";
        
        const string applicationEmailPasswordConfiguration = "SmtpErrorsConfiguration:GoogleApplicationPassword";
        const string sendGridApiKey = "SendGridOptions:ApiSecretKey";
        
        NetworkCredential networkCredential = new NetworkCredential
        {
            UserName = configuration[fromEmailConfiguration],
            Password = configuration[applicationEmailPasswordConfiguration]
        };
        
        SendGridClient sendGridClient = new SendGridClient(configuration[sendGridApiKey]);

        EmailConnectionInfo emailConnectionInfo = new EmailConnectionInfo
        {
            EmailSubject = configuration[subjectEmailConfiguration],
            FromEmail = configuration[fromEmailConfiguration],
            ToEmail = configuration[toEmailConfiguration],
            NetworkCredentials = networkCredential,
            SendGridClient = sendGridClient,
            FromName = configuration[fromNameEmailConfiguration],
            Port = configuration.GetValue<int>(portEmailConfiguration),
            EnableSsl = configuration.GetValue<bool>(sslEmailConfiguration)
        };

        return emailConnectionInfo;
    }
}