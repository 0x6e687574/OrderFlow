using OrderFlow.Inventory.Application.Events.Abstractions;

namespace OrderFlow.Inventory.Application.Events;

public sealed class ReservationFailedEvent : BaseEvent
{
    public Guid OrderId { get; set; }
    public string Reason { get; set; } = null!;
}