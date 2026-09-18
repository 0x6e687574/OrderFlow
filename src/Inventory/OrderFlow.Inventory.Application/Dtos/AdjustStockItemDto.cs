namespace OrderFlow.Inventory.Application.Dtos;

public class AdjustStockItemDto
{
    public string Sku { get; set; } = null!;
    public int Quantity { get; set; }
}