namespace OrderFlow.Order.Application.Dtos;

public class CreateOrderDto
{
    public string CustomerId { get; set; } = null!;
    public IReadOnlyCollection<CreateOrderLineDto> OrderLines { get; set; } = null!;
}