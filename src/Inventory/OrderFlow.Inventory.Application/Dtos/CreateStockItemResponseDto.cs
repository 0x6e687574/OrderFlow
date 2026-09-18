namespace OrderFlow.Inventory.Application.Dtos;

public class CreateStockItemResponseDto
{
    public string Sku { get; set; } = null!;
    public int QuantityOnHand { get; set; }
    public int QuantityReserved { get; set; }
}