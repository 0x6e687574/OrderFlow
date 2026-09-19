using OrderFlow.Order.Application.Events.Abstractions;

namespace OrderFlow.Order.Application.Events;

public sealed class ReservationSucceededEvent : BaseEvent
{
    public Guid OrderId { get; set; }
    public IReadOnlyCollection<ReservationSucceededOrderLineEvent> OrderLines { get; set; } = null!;
}