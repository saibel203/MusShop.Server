namespace MusShop.Domain.Model.Entities.HealthCheck;

public class MemoryCheckOptions
{
    public string MemoryStatus { get; set; } = string.Empty;
    public long Threshold { get; set; } = 1024L * 1024L * 1024L;
}