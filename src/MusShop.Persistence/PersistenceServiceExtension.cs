using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusShop.Domain.Services.Helpers;
using MusShop.Persistence.Seeds;

namespace MusShop.Persistence;

public static class PersistenceServiceExtension
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        string infrastructureConnectionString =
            ConnectionStringHelper.GetDataDbConnectionString(configuration);

        // Register InitializeDatabase Service
        services.AddScoped<InitializeDataDbContext>();

        // Register Data DbContext
        services.AddDbContext<MusShopDataDbContext>(options =>
            options.UseSqlServer(infrastructureConnectionString));

        return services;
    }
}