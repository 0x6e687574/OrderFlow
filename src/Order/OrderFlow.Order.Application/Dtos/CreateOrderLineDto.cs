namespace OrderFlow.Order.Application.Dtos;

public class CreateOrderLineDto
{
    public string Sku { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}