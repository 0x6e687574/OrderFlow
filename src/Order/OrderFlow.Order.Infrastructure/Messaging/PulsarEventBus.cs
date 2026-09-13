using System.Collections.Concurrent;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;
using OrderFlow.Order.Application.Abstractions.Messaging;

namespace OrderFlow.Order.Infrastructure.Messaging;

public class PulsarEventBus(IPulsarClient pulsarClient) : IEventBus
{
    private readonly ConcurrentDictionary<string, IProducer<string>> _producers = new();

    public async Task PublishAsync(string topic, string correlationId, string payload)
    {
        var producer = _producers.GetOrAdd(topic, newTopic =>
            pulsarClient
                .NewProducer(Schema.String)
                .Topic($"persistent://public/default/{newTopic}")
                .Create());

        await producer
            .NewMessage()
            .Key(correlationId)
            .Send(payload);
    }
}