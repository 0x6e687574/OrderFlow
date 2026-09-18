namespace OrderFlow.Inventory.Application.Dtos;

public class CreateStockItemDto
{
    public string Sku { get; set; } = null!;
    public int Quantity { get; set; }
}