using OrderFlow.Order.Domain.Enums;

namespace OrderFlow.Order.Domain.Entities;

public class Order
{
    public Guid Id  { get; private set; }
    public string CustomerId { get; private set; } = null!;
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public ICollection<OrderLine> OrderLines { get; } = [];
}