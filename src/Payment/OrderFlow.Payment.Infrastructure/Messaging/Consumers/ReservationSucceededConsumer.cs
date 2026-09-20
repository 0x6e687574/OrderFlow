using DotPulsar.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrderFlow.Payment.Application.Constants;
using OrderFlow.Payment.Application.Events;
using OrderFlow.Payment.Infrastructure.Messaging.Consumers.Abstractions;
using OrderFlow.Payment.Infrastructure.Messaging.Consumers.Handlers;
using OrderFlow.Payment.Infrastructure.Persistence;

namespace OrderFlow.Payment.Infrastructure.Messaging.Consumers;

public class ReservationSucceededConsumer(
    IServiceProvider serviceProvider,
    IPulsarClient client,
    ILogger<ReservationSucceededConsumer> logger)
    : BaseConsumer<ReservationSucceededEvent>(client, logger)
{
    protected override string Topic => TopicName.ReservationSucceeded;
    protected override string Subscription => SubscriptionName.PaymentService;

    protected override async Task HandleAsync(ReservationSucceededEvent payload, CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();

        await using var paymentDbContext = scope
            .ServiceProvider
            .GetRequiredService<PaymentDbContext>();

        var handler = new ReservationSucceededHandler(paymentDbContext);

        await handler.HandleAsync(payload, cancellationToken);
    }
}