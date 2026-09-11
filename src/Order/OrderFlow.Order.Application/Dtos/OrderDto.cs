namespace OrderFlow.Order.Application.Dtos;

public class OrderDto
{
    public string CustomerId { get; set; } = null!;
    public IReadOnlyCollection<OrderLineDto> OrderLines { get; set; } = null!;
}