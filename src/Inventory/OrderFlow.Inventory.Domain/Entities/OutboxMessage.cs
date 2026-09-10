using System.Text.Json;
using OrderFlow.Inventory.Domain.Exceptions;

namespace OrderFlow.Inventory.Domain.Entities;

public sealed class OutboxMessage
{
    public long Id { get; private set; }
    public Guid EventId { get; private set; }
    public string Topic { get; private set; } = null!;
    public JsonDocument Payload { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? PublishedAt { get; private set; }

    private OutboxMessage()
    {
    }

    public static OutboxMessage Create(Guid eventId, string topic, JsonDocument payload)
        => new()
        {
            EventId = eventId,
            Topic = topic,
            Payload = payload,
            CreatedAt = DateTime.UtcNow
        };

    public void Update(DateTime publishedAt)
    {
        if (publishedAt <= CreatedAt)
        {
            throw new InvalidDateTimeException();
        }

        PublishedAt = publishedAt;
    }
}