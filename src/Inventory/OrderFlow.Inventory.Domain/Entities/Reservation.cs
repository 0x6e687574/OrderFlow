using OrderFlow.Inventory.Domain.Enums;
using OrderFlow.Inventory.Domain.Exceptions;

namespace OrderFlow.Inventory.Domain.Entities;

public sealed class Reservation
{
    public long Id { get; private set; }
    public Guid OrderId { get; private set; }
    public string Sku { get; private set; } = null!;

    public int Quantity
    {
        get;
        private set
        {
            if (value <= 0)
            {
                throw new InvalidQuantityException();
            }

            field = value;
        }
    }

    public InventoryStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    private Reservation()
    {
    }

    public static Reservation Create(Guid orderId, string sku, int quantity)
        => new()
        {
            OrderId = orderId,
            Sku = sku,
            Quantity = quantity,
            Status = InventoryStatus.Active,
            CreatedAt = DateTime.UtcNow
        };

    public void Release()
        => SetStatus(InventoryStatus.Released, InventoryStatus.Active);

    public void Consume()
        => SetStatus(InventoryStatus.Consumed, InventoryStatus.Active);

    private bool IsValidStatuses(InventoryStatus[] statuses)
        => statuses.Contains(Status);

    private void EnsureStatus(params InventoryStatus[] allowedCurrentStatuses)
    {
        if (!IsValidStatuses(allowedCurrentStatuses))
        {
            throw new InvalidInventoryStatusException();
        }
    }

    private void SetStatus(InventoryStatus status, params InventoryStatus[] allowedCurrentStatuses)
    {
        EnsureStatus(allowedCurrentStatuses);

        Status = status;
    }
}