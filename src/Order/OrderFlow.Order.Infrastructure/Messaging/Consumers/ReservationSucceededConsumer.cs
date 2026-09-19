using DotPulsar.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderFlow.Order.Application.Constants;
using OrderFlow.Order.Application.Events;
using OrderFlow.Order.Infrastructure.Messaging.Consumers.Abstractions;
using OrderFlow.Order.Infrastructure.Messaging.Consumers.Handlers;
using OrderFlow.Order.Infrastructure.Persistence;

namespace OrderFlow.Order.Infrastructure.Messaging.Consumers;

public class ReservationSucceededConsumer(
    IServiceProvider serviceProvider,
    IPulsarClient client,
    ILogger<ReservationSucceededConsumer> logger)
    : BaseConsumer<ReservationSucceededEvent>(client, logger)
{
    protected override string Topic => TopicName.ReservationSucceeded;
    protected override string Subscription => SubscriptionName.OrderService;

    protected override async Task HandleAsync(ReservationSucceededEvent payload, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        await using var orderDbContext = scope
            .ServiceProvider
            .GetRequiredService<OrderDbContext>();

        var handler = new ReservationSucceededHandler(orderDbContext);

        await handler.HandleAsync(payload, cancellationToken);
    }
}