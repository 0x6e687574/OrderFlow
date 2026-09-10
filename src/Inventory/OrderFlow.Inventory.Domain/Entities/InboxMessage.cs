namespace OrderFlow.Inventory.Domain.Entities;

public sealed class InboxMessage
{
    public Guid EventId { get; private set; }
    public DateTime ProcessedAt { get; private set; }

    private InboxMessage()
    {
    }

    public static InboxMessage Create(Guid eventId, DateTime processedAt)
        => new()
        {
            EventId = eventId,
            ProcessedAt = processedAt
        };
}