namespace OrderFlow.Client.Dtos.Order;

public class CreateOrderLineRequestDto
{
    public string Sku { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
}