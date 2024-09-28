using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using MusShop.Domain.Model.Entities.HealthCheck;

namespace MusShop.Api.HealthChecks;

public class MemoryHealthCheck : IHealthCheck
{
    private readonly IOptionsMonitor<MemoryCheckOptions> _options;

    public MemoryHealthCheck(IOptionsMonitor<MemoryCheckOptions> options)
    {
        _options = options;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
        CancellationToken cancellationToken = new())
    {
        MemoryCheckOptions options = _options.Get(context.Registration.Name);

        long allocated = GC.GetTotalMemory(forceFullCollection: false);
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "AllocatedBytes", allocated },
            { "Gen0Collections", GC.CollectionCount(0) },
            { "Gen1Collections", GC.CollectionCount(1) },
            { "Gen2Collections", GC.CollectionCount(2) }
        };
        HealthStatus status =
            allocated < options.Threshold ? HealthStatus.Healthy : HealthStatus.Unhealthy;

        return Task.FromResult(new HealthCheckResult(
            status,
            description: "Reports degraded status if allocated bytes " +
                         $">= {options.Threshold} bytes.",
            exception: null,
            data: data));
    }
}