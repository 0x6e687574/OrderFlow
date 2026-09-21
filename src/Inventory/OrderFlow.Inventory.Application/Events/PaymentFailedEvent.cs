using OrderFlow.Inventory.Application.Events.Abstractions;

namespace OrderFlow.Inventory.Application.Events;

public sealed class PaymentFailedEvent : BaseEvent
{
    public Guid OrderId { get; set; }
    public string Reason { get; set; } = null!;
}