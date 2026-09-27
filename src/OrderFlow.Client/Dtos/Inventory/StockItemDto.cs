namespace OrderFlow.Client.Dtos.Inventory;

public record StockItemDto(
    string Sku,
    int QuantityOnHand,
    int QuantityReserved,
    int Available);