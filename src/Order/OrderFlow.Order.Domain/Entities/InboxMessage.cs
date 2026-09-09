namespace OrderFlow.Order.Domain.Entities;

public class InboxMessage
{
    public Guid EventId { get; private set; }
    public DateTime ProcessedAt { get; private set; }
}