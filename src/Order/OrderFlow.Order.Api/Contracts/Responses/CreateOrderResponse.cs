namespace OrderFlow.Order.Api.Contracts.Responses;

public record CreateOrderResponse(
    Guid OrderId,
    Guid CorrelationId,
    string Status);