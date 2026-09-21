using OrderFlow.Order.Application.Events.Abstractions;

namespace OrderFlow.Order.Application.Events;

public sealed class PaymentSucceededEvent : BaseEvent
{
    public Guid OrderId { get; set; }
    public Guid PaymentId { get; set; }
    public decimal Amount { get; set; }
}