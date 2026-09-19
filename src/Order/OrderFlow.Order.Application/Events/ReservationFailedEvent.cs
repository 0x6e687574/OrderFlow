using OrderFlow.Order.Application.Events.Abstractions;

namespace OrderFlow.Order.Application.Events;

public sealed class ReservationFailedEvent : BaseEvent
{
    public Guid OrderId { get; set; }
    public string Reason { get; set; } = null!;
}