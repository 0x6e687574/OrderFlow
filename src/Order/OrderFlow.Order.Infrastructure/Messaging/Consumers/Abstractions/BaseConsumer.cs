using System.Text.Json;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using Microsoft.Extensions.Hosting;
using OrderFlow.Order.Application.Constants;
using OrderFlow.Order.Application.Events.Abstractions;
using OrderFlow.Order.Infrastructure.Exceptions;

namespace OrderFlow.Order.Infrastructure.Messaging.Consumers.Abstractions;

public abstract class BaseConsumer<T>(IPulsarClient client) : BackgroundService
    where T : BaseEvent
{
    private const int MaxRetryAttempts = 3;

    protected abstract string Topic { get; }
    protected abstract string Subscription { get; }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using var consumer = client
            .NewConsumer(Schema.String)
            .Topic($"persistent://public/default/{Topic}")
            .SubscriptionName(Subscription)
            .SubscriptionType(SubscriptionType.KeyShared)
            .InitialPosition(SubscriptionInitialPosition.Earliest)
            .Create();

        await using var producer = client
            .NewProducer(Schema.String)
            .Topic($"persistent://public/default/{TopicName.DeadLetter}")
            .Create();

        await foreach (var message in consumer.Messages(stoppingToken))
        {
            try
            {
                var payload = JsonSerializer.Deserialize<T>(message.Value())
                    ?? throw new DeserializeFailedException();

                await HandleAsync(payload, stoppingToken);

                await consumer.Acknowledge(message, stoppingToken);
            }
            catch
            {
                if (!CanRetry(message.RedeliveryCount))
                {
                    await producer
                        .NewMessage()
                        .Key(message.Key ?? throw new MessageKeyNotFoundException())
                        .Send(message.Value(), stoppingToken);

                    await consumer.Acknowledge(message, stoppingToken);
                }
            }
        }
    }

    protected abstract Task HandleAsync(T payload, CancellationToken cancellationToken);

    private static bool CanRetry(uint retryCount)
        => retryCount < MaxRetryAttempts;
}