namespace OrderFlow.Order.Application.Events;

public class OrderPlacedOrderLineEvent
{
    public string Sku { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}