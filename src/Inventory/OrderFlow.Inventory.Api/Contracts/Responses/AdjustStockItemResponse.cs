namespace OrderFlow.Inventory.Api.Contracts.Responses;

public record AdjustStockItemResponse(
    string Sku,
    int QuantityOnHand,
    int QuantityReserved,
    int Available);