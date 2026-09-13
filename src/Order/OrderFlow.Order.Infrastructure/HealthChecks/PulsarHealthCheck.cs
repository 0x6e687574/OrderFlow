using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace OrderFlow.Order.Infrastructure.HealthChecks;

public class PulsarHealthCheck(
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration)
    : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var client = httpClientFactory.CreateClient();

        var adminUrl = configuration["Pulsar:AdminUrl"];

        var response = await client.GetAsync(
            $"{adminUrl}/admin/v2/brokers/health",
            cancellationToken);

        return response.IsSuccessStatusCode
            ? HealthCheckResult.Healthy()
            : HealthCheckResult.Unhealthy();
    }
}