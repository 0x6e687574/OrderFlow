namespace OrderFlow.Inventory.Api.Contracts.Responses;

public record CreateStockItemResponse(string Sku, int QuantityOnHand, int QuantityReserved);