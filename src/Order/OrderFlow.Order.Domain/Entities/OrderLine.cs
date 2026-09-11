using OrderFlow.Order.Domain.Exceptions;

namespace OrderFlow.Order.Domain.Entities;

public sealed class OrderLine
{
    public long Id { get; private set; }
    public Guid OrderId { get; private set; }

    public string Sku
    {
        get;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new InvalidSkuException();
            }
            
            field = value;
        }
    } = null!;

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

    public decimal UnitPrice
    {
        get;
        private set
        {
            if (value <= 0.00m)
            {
                throw new InvalidUnitPriceException();
            }

            field = value;
        }
    }

    private OrderLine()
    {
    }

    public static OrderLine Create(
        Guid orderId,
        string sku,
        int quantity,
        decimal unitPrice)
        => new()
        {
            OrderId = orderId,
            Sku = sku,
            Quantity = quantity,
            UnitPrice = unitPrice
        };
}