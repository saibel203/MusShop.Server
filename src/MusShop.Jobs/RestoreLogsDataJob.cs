using MusShop.Domain.Model.InfrastructureServiceAbstractions;

namespace MusShop.Jobs;

public class RestoreLogsDataJob
{
    private readonly IRestoreLogsTableService _restoreLogsTableService;

    public RestoreLogsDataJob(IRestoreLogsTableService restoreLogsTableService)
    {
        _restoreLogsTableService = restoreLogsTableService;
    }

    public async Task RestoreLogsData()
    {
        await _restoreLogsTableService.RestoreTableLogs();
    }
}