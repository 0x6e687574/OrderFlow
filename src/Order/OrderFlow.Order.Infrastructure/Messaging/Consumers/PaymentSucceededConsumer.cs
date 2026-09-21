using DotPulsar.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderFlow.Order.Application.Constants;
using OrderFlow.Order.Application.Events;
using OrderFlow.Order.Infrastructure.Messaging.Consumers.Abstractions;
using OrderFlow.Order.Infrastructure.Messaging.Consumers.Handlers;
using OrderFlow.Order.Infrastructure.Persistence;

namespace OrderFlow.Order.Infrastructure.Messaging.Consumers;

public class PaymentSucceededConsumer(
    IServiceProvider serviceProvider,
    IPulsarClient client,
    ILogger<PaymentSucceededConsumer> logger)
    : BaseConsumer<PaymentSucceededEvent>(client, logger)
{
    protected override string Topic => TopicName.PaymentSucceeded;
    protected override string Subscription => SubscriptionName.OrderService;

    protected override async Task HandleAsync(PaymentSucceededEvent payload, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        await using var orderDbContext = scope
            .ServiceProvider
            .GetRequiredService<OrderDbContext>();

        var handler = new PaymentSucceededHandler(orderDbContext);

        await handler.HandleAsync(payload, cancellationToken);
    }
}