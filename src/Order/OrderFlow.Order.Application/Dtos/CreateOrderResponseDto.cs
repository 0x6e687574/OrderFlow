namespace OrderFlow.Order.Application.Dtos;

public class CreateOrderResponseDto
{
    public Guid OrderId { get; set; }
    public Guid CorrelationId { get; set; }
    public string Status { get; set; } = null!;
}