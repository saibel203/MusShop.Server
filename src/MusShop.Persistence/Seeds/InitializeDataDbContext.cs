using Microsoft.EntityFrameworkCore;
using Serilog;

namespace MusShop.Persistence.Seeds;

public class InitializeDataDbContext
{
    private readonly MusShopDataDbContext _context;
    private readonly ILogger _logger = Log.ForContext<InitializeDataDbContext>();

    public InitializeDataDbContext(MusShopDataDbContext context)
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

            _logger.Information("The data database has been successfully initialized");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An error occurred while trying to initialize the data database.");
            throw;
        }
    }
}