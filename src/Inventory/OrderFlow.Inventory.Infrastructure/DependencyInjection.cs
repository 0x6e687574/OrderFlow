using DotPulsar;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OrderFlow.Inventory.Application.Abstractions.Messaging;
using OrderFlow.Inventory.Application.Abstractions.Repositories;
using OrderFlow.Inventory.Application.Abstractions.UnitOfWorks;
using OrderFlow.Inventory.Infrastructure.HealthChecks;
using OrderFlow.Inventory.Infrastructure.Messaging;
using OrderFlow.Inventory.Infrastructure.Messaging.Consumers;
using OrderFlow.Inventory.Infrastructure.Messaging.Providers;
using OrderFlow.Inventory.Infrastructure.Persistence;
using OrderFlow.Inventory.Infrastructure.Persistence.Repository;
using OrderFlow.Inventory.Infrastructure.Persistence.UnitOfWorks;

namespace OrderFlow.Inventory.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton(_ => PulsarClient
            .Builder()
            .ServiceUrl(new Uri(configuration["Pulsar:ServiceUrl"]!))
            .Build());

        services.AddSingleton<IEventBus, PulsarEventBus>();

        services.AddScoped<IStockItemRepository, StockItemRepository>();
        services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddHostedService<InventoryProvider>();

        services.AddHostedService<OrderPlacedConsumer>();
        services.AddHostedService<PaymentFailedConsumer>();
        services.AddHostedService<PaymentSucceededConsumer>();

        services.AddHttpClient();

        services.AddHealthChecks()
            .AddNpgSql(
                connectionString: configuration.GetConnectionString("DefaultConnection")!,
                name: "postgres",
                failureStatus: HealthStatus.Unhealthy)
            .AddCheck<PulsarHealthCheck>(
                name: "pulsar",
                failureStatus: HealthStatus.Unhealthy);

        return services;
    }
}