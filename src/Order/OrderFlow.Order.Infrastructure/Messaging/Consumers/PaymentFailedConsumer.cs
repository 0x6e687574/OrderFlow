using DotPulsar.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderFlow.Order.Application.Constants;
using OrderFlow.Order.Application.Events;
using OrderFlow.Order.Infrastructure.Messaging.Consumers.Abstractions;
using OrderFlow.Order.Infrastructure.Messaging.Consumers.Handlers;
using OrderFlow.Order.Infrastructure.Persistence;

namespace OrderFlow.Order.Infrastructure.Messaging.Consumers;

public class PaymentFailedConsumer(
    IServiceProvider serviceProvider,
    IPulsarClient client,
    ILogger<PaymentFailedConsumer> logger)
    : BaseConsumer<PaymentFailedEvent>(client, logger)
{
    protected override string Topic => TopicName.PaymentFailed;
    protected override string Subscription => SubscriptionName.OrderService;

    protected override async Task HandleAsync(PaymentFailedEvent payload, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        await using var orderDbContext = scope
            .ServiceProvider
            .GetRequiredService<OrderDbContext>();

        var handler = new PaymentFailedHandler(orderDbContext);

        await handler.HandleAsync(payload, cancellationToken);
    }
}