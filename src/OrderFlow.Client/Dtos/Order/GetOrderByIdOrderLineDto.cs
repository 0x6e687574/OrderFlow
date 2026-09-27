namespace OrderFlow.Client.Dtos.Order;

public record GetOrderByIdOrderLineDto(
    string Sku,
    int Quantity,
    decimal UnitPrice);