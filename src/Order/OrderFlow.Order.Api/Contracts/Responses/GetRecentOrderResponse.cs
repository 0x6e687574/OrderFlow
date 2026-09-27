namespace OrderFlow.Order.Api.Contracts.Responses;

public record GetRecentOrderResponse(
    Guid OrderId,
    string CustomerId,
    string Status,
    decimal TotalAmount,
    DateTime CreatedAt,
    DateTime UpdatedAt);