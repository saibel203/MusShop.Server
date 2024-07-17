using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusShop.Infrastructure.Database;
using SendGrid;
using Serilog.Events;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.Email;
using Serilog.Sinks.MSSqlServer;
using MusShop.Domain.Services.Helpers;

namespace MusShop.Infrastructure;

public static class InfrastructureServicesExtension
{
    public static IServiceCollection AddInfrastructureServices<TExceptionMiddleware>(
        this IServiceCollection services, IConfiguration configuration)
        where TExceptionMiddleware : class
    {
        const string outputTemplate =
            "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";

        string infrastructureConnectionString =
            ConnectionStringHelper.GetInfrastructureDbConnectionString(configuration);

        // Register And Setup Logger
        EmailConnectionInfo emailConnectionInfo = GetEmailConnectionInfoData(configuration);
        MSSqlServerSinkOptions msSqlSinkOptions = GetMsSqlSinkOptions();

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
            .WriteTo.Seq(
                serverUrl: "http://musshop-seq:5341",
                restrictedToMinimumLevel: LogEventLevel.Information)
            .WriteTo.Logger(logger => logger
                .Filter.ByIncludingOnly(Matching.FromSource<TExceptionMiddleware>())
                .WriteTo.Email(
                    connectionInfo: emailConnectionInfo,
                    restrictedToMinimumLevel: LogEventLevel.Error)
                .WriteTo.MSSqlServer(
                    connectionString: infrastructureConnectionString,
                    sinkOptions: msSqlSinkOptions,
                    restrictedToMinimumLevel: LogEventLevel.Error))
            .CreateLogger();

        services.AddSerilog();

        // Register Infrastructure DbContext
        services.AddDbContext<MusShopInfrastructureDbContext>(options =>
            options.UseSqlServer(infrastructureConnectionString));

        // Register SeedDatabase Service
        services.AddScoped<SeedInfrastructureDbContext>();

        return services;
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

    private static MSSqlServerSinkOptions GetMsSqlSinkOptions()
    {
        const string tableLogsName = "LogEventsData";

        MSSqlServerSinkOptions sinkOptions = new MSSqlServerSinkOptions
        {
            TableName = tableLogsName
        };

        return sinkOptions;
    }
}