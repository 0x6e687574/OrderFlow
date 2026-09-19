namespace OrderFlow.Inventory.Application.Events.Abstractions;

public abstract class BaseEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
}