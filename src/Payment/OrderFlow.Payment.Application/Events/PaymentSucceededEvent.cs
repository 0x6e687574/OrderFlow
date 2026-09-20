using OrderFlow.Payment.Application.Events.Abstractions;

namespace OrderFlow.Payment.Application.Events;

public sealed class PaymentSucceededEvent : BaseEvent
{
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
}