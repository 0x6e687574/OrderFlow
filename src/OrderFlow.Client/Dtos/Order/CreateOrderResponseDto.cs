namespace OrderFlow.Client.Dtos.Order;

public record CreateOrderResponseDto(Guid OrderId, string Status);