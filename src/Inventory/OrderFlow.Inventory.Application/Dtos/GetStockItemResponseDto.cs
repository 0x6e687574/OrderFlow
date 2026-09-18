namespace OrderFlow.Inventory.Application.Dtos;

public class GetStockItemResponseDto
{
    public string Sku { get; set; } = null!;
    public int QuantityOnHand { get; set; }
    public int QuantityReserved { get; set; }
    public int Available { get; set; }
}