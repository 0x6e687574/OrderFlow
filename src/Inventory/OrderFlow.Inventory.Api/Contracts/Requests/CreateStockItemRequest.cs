namespace OrderFlow.Inventory.Api.Contracts.Requests;

public record CreateStockItemRequest(string Sku, int Quantity);