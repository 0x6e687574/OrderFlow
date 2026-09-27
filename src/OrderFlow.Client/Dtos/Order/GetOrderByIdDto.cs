namespace OrderFlow.Client.Dtos.Order;

public record GetOrderByIdDto(
    Guid OrderId,
    string CustomerId,
    string Status,
    decimal TotalAmount,
    bool ReservationCompleted,
    bool PaymentCompleted,
    IReadOnlyCollection<GetOrderByIdOrderLineDto> OrderLines,
    DateTime CreatedAt,
    DateTime UpdatedAt);