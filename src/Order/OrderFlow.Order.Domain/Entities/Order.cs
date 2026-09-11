using OrderFlow.Order.Domain.Enums;
using OrderFlow.Order.Domain.Exceptions;

namespace OrderFlow.Order.Domain.Entities;

public sealed class Order
{
    private readonly List<OrderLine> _orderLines = [];

    public Guid Id { get; private set; }

    public string CustomerId
    {
        get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidCustomerIdException();
            }

            field = value;
        }
    } = null!;

    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public IReadOnlyCollection<OrderLine> OrderLines => _orderLines.AsReadOnly();

    private Order()
    {
    }

    public static Order Create(string customerId)
        => new()
        {
            Id = Guid.NewGuid(),
            CustomerId = customerId,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

    public void AddRange(IReadOnlyCollection<OrderLine> orderLines)
    {
        if (IsEmptyOrderLines(orderLines))
        {
            throw new EmptyOrderLinesException();
        }

        if (IsDuplicatedOrderLines(orderLines))
        {
            throw new DuplicatedOrderLinesException();
        }

        foreach (var orderLine in orderLines)
        {
            AddItem(orderLine);
        }

        CalculateTotalAmount();
    }

    public void Reserve()
        => SetStatus(OrderStatus.Reserving, OrderStatus.Pending);

    public void Charge()
        => SetStatus(OrderStatus.Charging, OrderStatus.Reserving);

    public void Confirm()
        => SetStatus(OrderStatus.Confirmed, OrderStatus.Charging);

    public void Cancel()
        => SetStatus(OrderStatus.Cancelled, OrderStatus.Reserving, OrderStatus.Charging);

    private static bool IsEmptyOrderLines(IReadOnlyCollection<OrderLine> orderLines)
        => orderLines is not { Count: > 0 };

    private static bool IsDuplicatedOrderLines(IReadOnlyCollection<OrderLine> orderLines)
        => orderLines.Count != orderLines.DistinctBy(ol => ol.Sku).Count();

    private bool IsValidStatuses(OrderStatus[] statuses)
        => statuses.Contains(Status);

    private void AddItem(OrderLine orderLine)
    {
        EnsureStatus(OrderStatus.Pending);

        _orderLines.Add(orderLine);
    }

    private void CalculateTotalAmount()
        => TotalAmount = _orderLines.Sum(ol => ol.Quantity * ol.UnitPrice);

    private void EnsureStatus(params OrderStatus[] allowedCurrentStatuses)
    {
        if (!IsValidStatuses(allowedCurrentStatuses))
        {
            throw new InvalidOrderStatusException();
        }
    }

    private void SetStatus(OrderStatus status, params OrderStatus[] allowedCurrentStatuses)
    {
        EnsureStatus(allowedCurrentStatuses);

        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
}