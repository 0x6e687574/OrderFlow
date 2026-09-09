using System.Text.Json;

namespace OrderFlow.Payment.Domain.Entities;

public class OutboxMessage
{
    public long Id { get; private set; }
    public Guid EventId { get; private set; }
    public string Topic { get; private set; } = null!;
    public JsonDocument Payload { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? PublishedAt { get; private set; }
}