namespace OrderFlow.Client.Dtos.Order;

public record GetRecentOrderDto(
    Guid OrderId,
    string CustomerId,
    string Status,
    decimal TotalAmount,
    DateTime CreatedAt,
    DateTime UpdatedAt);