namespace OrderFlow.Order.Api.Contracts.Requests;

public record CreateOrderLineRequest(string Sku, int Quantity, decimal UnitPrice);