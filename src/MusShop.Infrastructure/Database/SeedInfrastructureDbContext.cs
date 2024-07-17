using Microsoft.EntityFrameworkCore;
using Serilog;

namespace MusShop.Infrastructure.Database;

public class SeedInfrastructureDbContext
{
    private readonly MusShopInfrastructureDbContext _context;
    private readonly ILogger _logger = Log.ForContext<SeedInfrastructureDbContext>();

    public SeedInfrastructureDbContext(MusShopInfrastructureDbContext context)
    {
        _context = context;
    }

    public async Task InitializeDatabaseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }

            _logger.Information("The database has been successfully initialized");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An error occurred while trying to initialize the database.");
            throw;
        }
    }
}