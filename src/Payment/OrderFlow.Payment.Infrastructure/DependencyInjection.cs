using DotPulsar;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using OrderFlow.Payment.Application.Abstractions.Messaging;
using OrderFlow.Payment.Application.Abstractions.Repositories;
using OrderFlow.Payment.Application.Abstractions.UnitOfWorks;
using OrderFlow.Payment.Infrastructure.HealthChecks;
using OrderFlow.Payment.Infrastructure.Messaging;
using OrderFlow.Payment.Infrastructure.Messaging.Consumers;
using OrderFlow.Payment.Infrastructure.Messaging.Providers;
using OrderFlow.Payment.Infrastructure.Persistence;
using OrderFlow.Payment.Infrastructure.Persistence.Repository;
using OrderFlow.Payment.Infrastructure.Persistence.UnitOfWorks;

namespace OrderFlow.Payment.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PaymentDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton(_ => PulsarClient
            .Builder()
            .ServiceUrl(new Uri(configuration["Pulsar:ServiceUrl"]!))
            .Build());

        services.AddSingleton<IEventBus, PulsarEventBus>();

        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IOutboxMessageRepository, OutboxMessageRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddHostedService<PaymentProvider>();

        services.AddHostedService<ReservationSucceededConsumer>();

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