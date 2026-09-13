using DotPulsar;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderFlow.Order.Application.Abstractions.Messaging;
using OrderFlow.Order.Application.Abstractions.Repositories;
using OrderFlow.Order.Application.Abstractions.UnitOfWorks;
using OrderFlow.Order.Infrastructure.Messaging;
using OrderFlow.Order.Infrastructure.Messaging.Providers;
using OrderFlow.Order.Infrastructure.Persistence;
using OrderFlow.Order.Infrastructure.Persistence.Repositories;
using OrderFlow.Order.Infrastructure.Persistence.UnitOfWorks;

namespace OrderFlow.Order.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OrderDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton(_ => PulsarClient.Builder().Build());
        services.AddSingleton<IEventBus, PulsarEventBus>();

        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOutboxMessagesRepository, OutboxMessageRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddHostedService<OrderProvider>();

        return services;
    }
}