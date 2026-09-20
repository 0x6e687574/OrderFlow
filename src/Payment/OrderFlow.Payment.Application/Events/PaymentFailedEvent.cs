using OrderFlow.Payment.Application.Events.Abstractions;

namespace OrderFlow.Payment.Application.Events;

public sealed class PaymentFailedEvent : BaseEvent
{
    public Guid OrderId { get; set; }
    public string Reason { get; set; } = null!;
}