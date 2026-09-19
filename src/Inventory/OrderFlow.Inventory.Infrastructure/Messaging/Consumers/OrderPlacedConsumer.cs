using DotPulsar.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderFlow.Inventory.Application.Constants;
using OrderFlow.Inventory.Application.Events;
using OrderFlow.Inventory.Infrastructure.Messaging.Consumers.Abstractions;
using OrderFlow.Inventory.Infrastructure.Messaging.Consumers.Handlers;
using OrderFlow.Inventory.Infrastructure.Persistence;

namespace OrderFlow.Inventory.Infrastructure.Messaging.Consumers;

public class OrderPlacedConsumer(
    IServiceProvider serviceProvider,
    IPulsarClient client,
    ILogger<OrderPlacedConsumer> logger)
    : BaseConsumer<OrderPlacedEvent>(client, logger)
{
    protected override string Topic => TopicName.OrderPlaced;
    protected override string Subscription => SubscriptionName.InventoryService;

    protected override async Task HandleAsync(OrderPlacedEvent payload, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        await using var inventoryDbContext = scope
            .ServiceProvider
            .GetRequiredService<InventoryDbContext>();

        var handler = new OrderPlacedHandler(inventoryDbContext);

        await handler.HandleAsync(payload, cancellationToken);
    }
}