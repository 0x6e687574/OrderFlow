using DotPulsar.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderFlow.Inventory.Application.Constants;
using OrderFlow.Inventory.Application.Events;
using OrderFlow.Inventory.Infrastructure.Messaging.Consumers.Abstractions;
using OrderFlow.Inventory.Infrastructure.Messaging.Consumers.Handlers;
using OrderFlow.Inventory.Infrastructure.Persistence;

namespace OrderFlow.Inventory.Infrastructure.Messaging.Consumers;

public class PaymentFailedConsumer(
    IServiceProvider serviceProvider,
    IPulsarClient client,
    ILogger<PaymentFailedConsumer> logger)
    : BaseConsumer<PaymentFailedEvent>(client, logger)
{
    protected override string Topic => TopicName.PaymentFailed;
    protected override string Subscription => SubscriptionName.InventoryService;

    protected override async Task HandleAsync(PaymentFailedEvent payload, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        await using var inventoryDbContext = scope
            .ServiceProvider
            .GetRequiredService<InventoryDbContext>();

        var handler = new PaymentFailedHandler(inventoryDbContext);

        await handler.HandleAsync(payload, cancellationToken);
    }
}