using OrderFlow.Inventory.Domain.Enums;

namespace OrderFlow.Inventory.Domain.Entities;

public class Reservation
{
    public long Id { get; private set; }
    public Guid OrderId { get; private set; }
    public string Sku { get; private set; } = null!;
    public int Quantity { get; private set; }
    public InventoryStatus Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
}