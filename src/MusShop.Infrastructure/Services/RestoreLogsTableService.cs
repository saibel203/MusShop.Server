using Microsoft.EntityFrameworkCore;
using MusShop.Domain.Model.InfrastructureServiceAbstractions;
using MusShop.Infrastructure.Database;
using Serilog;

namespace MusShop.Infrastructure.Services;

public class RestoreLogsTableService : IRestoreLogsTableService
{
    private readonly MusShopInfrastructureDbContext _infrastructureDbContext;
    private readonly ILogger _logger = Log.ForContext<RestoreLogsTableService>();

    public RestoreLogsTableService(MusShopInfrastructureDbContext infrastructureDbContext)
    {
        _infrastructureDbContext = infrastructureDbContext;
    }

    public async Task RestoreTableLogs()
    {
        try
        {
            await _infrastructureDbContext.Database.ExecuteSqlRawAsync("[dbo].[Infrastructure_TruncateLogsTable]");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "An error was encountered while trying to clear the LogEvents log table");
        }
    }
}