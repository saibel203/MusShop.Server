using Microsoft.Extensions.Configuration;

namespace MusShop.Domain.Services.Helpers;

public static class ConnectionStringHelper
{
    public static string GetInfrastructureDbConnectionString(IConfiguration configuration)
    {
        string server = configuration["InfrastructureDBServer"] ?? "localhost";
        string port = configuration["InfrastructureDBPort"] ?? "1433";
        string user = configuration["InfrastructureDBUser"] ?? "SA";
        string password = configuration["InfrastructureDBPassword"] ?? "123456a@";
        string databaseName = configuration["InfrastructureDBName"] ?? "MusShop.Infrastructure";

        string resultString =
            $"Server={server},{port};Initial Catalog={databaseName};User ID={user};Password={password};TrustServerCertificate=True";

        return resultString;
    }
}