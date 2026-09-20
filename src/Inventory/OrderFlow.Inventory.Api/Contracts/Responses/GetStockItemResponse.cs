namespace OrderFlow.Inventory.Api.Contracts.Responses;

public record GetStockItemResponse(
    string Sku,
    int QuantityOnHand,
    int QuantityReserved,
    int Available);