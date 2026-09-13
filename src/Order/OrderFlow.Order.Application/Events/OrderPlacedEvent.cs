using OrderFlow.Order.Application.Events.Abstractions;

namespace OrderFlow.Order.Application.Events;

public sealed class OrderPlacedEvent :  BaseEvent
{
    public Guid OrderId { get; set; }
    public string CustomerId { get; set; } = null!;
    public IReadOnlyCollection<OrderPlacedOrderLineEvent> OrderLines { get; set; } = null!;
    public decimal TotalAmount { get; set; }
}