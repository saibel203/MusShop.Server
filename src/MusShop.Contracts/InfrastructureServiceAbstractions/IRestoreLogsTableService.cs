namespace MusShop.Contracts.InfrastructureServiceAbstractions;

public interface IRestoreLogsTableService
{
    Task RestoreTableLogs();
}