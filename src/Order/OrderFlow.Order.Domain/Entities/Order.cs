using OrderFlow.Order.Domain.Enums;
using OrderFlow.Order.Domain.Exceptions;

namespace OrderFlow.Order.Domain.Entities;

public class Order
{
    private readonly List<OrderLine> _orderLines = [];

    public Guid Id { get; private set; }
    public string CustomerId { get; private set; } = null!;
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyCollection<OrderLine> OrderLines => _orderLines.AsReadOnly();

    private Order()
    {
        Id = Guid.NewGuid();
        Status = OrderStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    public static Order Create(string customerId, IEnumerable<OrderLine> orderLines)
    {
        var order = new Order();

        foreach (var orderLine in orderLines)
        {
            order.AddItem(orderLine);
        }


    }

    public void AddItem(OrderLine orderLine)
    {
        if (Status is not OrderStatus.Pending)
        {
            throw new InvalidOrderStatusException();
        }

        _orderLines.Add(orderLine);
    }
}