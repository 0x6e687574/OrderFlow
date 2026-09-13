using System.Text.Json;

namespace OrderFlow.Order.Domain.Entities;

public sealed class OutboxMessage
{
    public long Id { get; private set; }
    public Guid EventId { get; private set; }
    public string Topic { get; private set; } = null!;
    public JsonDocument Payload { get; private set; } = null!;
    public Guid CorrelationId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? PublishedAt { get; private set; }

    private OutboxMessage()
    {
    }

    public static OutboxMessage Create(
        string topic,
        JsonDocument payload,
        Guid correlationId)
        => new()
        {
            EventId = Guid.NewGuid(),
            Topic = topic,
            Payload = payload,
            CorrelationId = correlationId,
            CreatedAt = DateTime.UtcNow
        };

    public void Publish() => PublishedAt = DateTime.UtcNow;
}