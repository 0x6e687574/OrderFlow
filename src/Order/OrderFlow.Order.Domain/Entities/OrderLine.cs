namespace OrderFlow.Order.Domain.Entities;

public class OrderLine
{
    public long Id { get; private set; }
    public Guid OrderId  { get; private set; }
    public string Sku { get; private set; } = null!;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }

    public Order Order { get; private set; } = null!;
}