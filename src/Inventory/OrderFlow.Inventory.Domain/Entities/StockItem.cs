namespace OrderFlow.Inventory.Domain.Entities;

public class StockItem
{
    public string Sku { get; private set; } = null!;
    public int QuantityOnHand { get; private set; }
    public int QuantityReserved { get; private set; }
}