using OrderFlow.Inventory.Application.Events.Abstractions;

namespace OrderFlow.Inventory.Application.Events;

public sealed class OrderPlacedEvent : BaseEvent
{
    public Guid OrderId { get; set; }
    public string CustomerId { get; set; } = null!;
    public IReadOnlyCollection<OrderPlacedOrderLineEvent> OrderLines { get; set; } = null!;
    public decimal TotalAmount { get; set; }
}