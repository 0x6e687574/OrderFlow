namespace OrderFlow.Order.Api.Contracts.Requests;

public record CreateOrderRequest(string CustomerId, IReadOnlyCollection<CreateOrderLineRequest> OrderLines);