using System.Text.Json;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OrderFlow.Payment.Application.Constants;
using OrderFlow.Payment.Application.Events.Abstractions;
using OrderFlow.Payment.Infrastructure.Exceptions;

namespace OrderFlow.Payment.Infrastructure.Messaging.Consumers.Abstractions;

public abstract class BaseConsumer<T>(
    IPulsarClient client,
    ILogger logger)
    : BackgroundService
    where T : BaseEvent
{
    private const int MaxRetryAttempts = 3;

    protected abstract string Topic { get; }
    protected abstract string Subscription { get; }

    protected sealed override async Task ExecuteAsync(CancellationToken stoppingToken)
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
            catch (Exception ex)
            {
                if (!CanRetry(message.RedeliveryCount))
                {
                    await producer
                        .NewMessage()
                        .Key(message.Key ?? throw new MessageKeyNotFoundException())
                        .Send(message.Value(), stoppingToken);

                    await consumer.Acknowledge(message, stoppingToken);
                }
                else
                {
                    logger.LogError(ex, "An error occurred while processing a message!");

                    await consumer.RedeliverUnacknowledgedMessages([message.MessageId], stoppingToken);
                }
            }
        }
    }

    protected abstract Task HandleAsync(T payload, CancellationToken cancellationToken);

    private static bool CanRetry(uint retryCount)
        => retryCount < MaxRetryAttempts;
}