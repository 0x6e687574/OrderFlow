using OrderFlow.Payment.Application.Events.Abstractions;

namespace OrderFlow.Payment.Application.Events;

public sealed class ReservationSucceededEvent : BaseEvent
{
    public Guid OrderId { get; set; }
    public IReadOnlyCollection<ReservationSucceededOrderLineEvent> OrderLines { get; set; } = null!;
}