using OrderFlow.Inventory.Application.Events.Abstractions;

namespace OrderFlow.Inventory.Application.Events;

public sealed class ReservationSucceededEvent : BaseEvent
{
    public Guid OrderId { get; set; }
    public IReadOnlyCollection<ReservationSucceededOrderLineEvent> OrderLines { get; set; } = null!;
}