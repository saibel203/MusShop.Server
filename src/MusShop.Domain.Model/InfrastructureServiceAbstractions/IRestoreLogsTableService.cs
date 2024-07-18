namespace MusShop.Domain.Model.InfrastructureServiceAbstractions;

public interface IRestoreLogsTableService
{
    Task RestoreTableLogs();
}