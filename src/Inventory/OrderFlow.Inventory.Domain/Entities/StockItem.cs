using OrderFlow.Inventory.Domain.Exceptions;

namespace OrderFlow.Inventory.Domain.Entities;

public sealed class StockItem
{
    public string Sku { get; private set; } = null!;

    public int QuantityOnHand
    {
        get;
        private set
        {
            if (value < 0)
            {
                throw new InvalidQuantityOnHandException();
            }

            field = value;
        }
    }

    public int QuantityReserved
    {
        get;
        private set
        {
            if (!IsInBoundary(value))
            {
                throw new InvalidQuantityReservedException();
            }

            field = value;
        }
    }

    public uint Version { get; private set; }

    private StockItem()
    {
    }

    public static StockItem Create(string sku, int quantityOnHand)
        => new()
        {
            Sku = sku,
            QuantityOnHand = quantityOnHand
        };

    public void Reserve(int quantity) => QuantityReserved += quantity;

    public void Cancel(int quantity) => QuantityReserved -= quantity;

    public void Consume(int quantity)
    {
        QuantityOnHand -= quantity;
        QuantityReserved -= quantity;
    }

    public void Adjust(int quantity) => QuantityOnHand = quantity;

    private bool IsInBoundary(int quantityReserved)
        => quantityReserved >= 0 && quantityReserved <= QuantityOnHand;
}